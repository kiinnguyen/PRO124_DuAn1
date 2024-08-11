using NavMeshPlus.Extensions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KingSkeletonManager : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] int health;
    [SerializeField] int damage;
    public bool isDead;
    [Header("Components")]
    [SerializeField] KingSkeletonMovement kingSkeletonMovement;
    [SerializeField] Animator anim;
    [Header("Children")]
    [SerializeField] Slider healthBar;
    [Header("GameObjects")]
    [SerializeField] GameObject exitGate;
    public Transform spawnPOS;
    [Header("Perfabs")]
    [SerializeField] GameObject slime;
    [Header("Attack Settings")]
    [SerializeField] private float attackCooldown = 2f; // Thời gian cooldown sau khi tấn công
    private bool isAttacking;
    private Coroutine attackCoroutine;
    // skills
    private bool isUsingSkill;
    // Heal skill
    private bool usedHealingskill;
    private void Start()
    {
        usedHealingskill = false;
        isUsingSkill = false;
        kingSkeletonMovement = GetComponent<KingSkeletonMovement>();
        anim = GetComponent<Animator>();

        if (healthBar != null)
        {
            healthBar.maxValue = health;
            healthBar.value = health;
        }
    }

    public void StartCombat()
    {
        kingSkeletonMovement.StartMove();
        StartCoroutine(SpawnCoroutine());

    }

    public void StopCombat()
    {
        kingSkeletonMovement.StopMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (attackCoroutine == null)
            {
                attackCoroutine = StartCoroutine(AttackPlayer());
            }
        }
    }
    private IEnumerator AttackPlayer()
    {
        while (!isDead)
        {
            // Bắt đầu tấn công
            anim.SetTrigger("Melee");
            isAttacking = true;
            kingSkeletonMovement.UseSkill(true);

            // Đợi cho đến khi hoạt ảnh hoàn thành
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

            // Kiểm tra nếu player vẫn còn trong vùng va chạm
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f, LayerMask.GetMask("Player"));

            foreach (Collider2D collider in colliders)
            {
                // Lấy tag của collider
                if(collider.tag == "Player")
                {
                    Debug.Log("Attack by Boss");
                    KnockBack knockBack = collider.GetComponent<KnockBack>();

                    if (knockBack != null)
                    {
                        Vector2 knockback = (collider.transform.position - transform.position).normalized;
                        knockBack.ApplyKnockback(knockback);
                        collider.SendMessage("TakeDamage", damage);
                    }
                }
            }

            // Cooldown tấn công
            yield return new WaitForSeconds(attackCooldown);

            isAttacking = false;
            kingSkeletonMovement.UseSkill(false);
        }
        attackCoroutine = null;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && attackCoroutine != null)
        {
            //StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isUsingSkill) return;
        health -= damage;

        if (health <= 0)
        {
            healthBar.value = 0;
            anim.SetTrigger("Death");
            Death();
        }
        else
        {
            healthBar.value = health;
        }

        if (health <=  healthBar.maxValue * 0.4)
        {
            if (!usedHealingskill)
            StartCoroutine(Skil_Heal());
        }
    }

    IEnumerator Skil_Heal()
    {
        // set using skill
        isUsingSkill = true;

        usedHealingskill = true;
        kingSkeletonMovement.UseSkill(true);
        anim.SetTrigger("Heal");
        float healvalue = healthBar.maxValue * 0.5f;
        health += (int)healvalue;
        healthBar.value = health;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        for (int i = 0; i < 2; i++)
        {
            Instantiate(slime, spawnPOS.position, Quaternion.identity);
        }

        // buff
        damage += 20;
        kingSkeletonMovement.UseSkill(false);

        isUsingSkill = false;
        yield return null;
    }

    
    private void Death()
    {
        isDead = true;
        kingSkeletonMovement.StopMove();
        GetComponent<Collider2D>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        exitGate.SetActive(true);
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            Instantiate(slime, transform.position, Quaternion.identity);
           
        }
    }
}
