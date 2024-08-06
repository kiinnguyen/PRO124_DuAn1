using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KingSkeletonManager : MonoBehaviour
{
    [Header("Component")]
    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject slimePrefab;
    private KingSkeletonMovement movement;
    [SerializeField] Slider healthBar;

    [Header("Stats")]
    public float health = 1000f;
    private float maxHealth;
    public float attackCooldown = 3f;
    private float nextAttackTime;
    public float skillDuration = 7f;
    private float skillCooldown = 10f;
    private float nextSkillTime;
    private bool isUsingSkill;
    private bool isDead;

    private bool isAttacking = false;

    [Header("Information")]
    [SerializeField] int damage;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        maxHealth = health;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }

        nextAttackTime = Time.time;
        nextSkillTime = Time.time + skillCooldown;
        movement = GetComponent<KingSkeletonMovement>();
    }

    void Update()
    {
        if (isDead) return;

        if (health <= 500 && !IsInvoking(nameof(HealRoutine)))
        {
            Invoke(nameof(HealRoutine), 30f);
        }

        if (isUsingSkill)
        {
            SkillAttack();
            return;
        }

        if (Time.time >= nextSkillTime)
        {
            StartCoroutine(UseSkill());
        }

        movement.MoveTo(player);
    }

    IEnumerator UseSkill()
    {
        isUsingSkill = true;
        nextSkillTime = Time.time + skillCooldown + skillDuration;
        animator.SetTrigger("Skill1");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > 1f && distance < 2f)
        {
            PlayerManager.FindObjectOfType<PlayerManager>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(skillDuration);
        isUsingSkill = false;
    }

    void SkillAttack()
    {
        movement.MoveTo(player);
    }

    void HealRoutine()
    {
        if (isDead) return;

        float healAmount = maxHealth * 0.1f;
        health = Mathf.Min(health + healAmount, maxHealth);
        animator.SetTrigger("Heal");

        for (int i = 0; i < 2; i++)
        {
            Instantiate(slimePrefab, transform.position, Quaternion.identity);
        }

        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        agent.ResetPath();
        animator.SetTrigger("Hurt");
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        UpdateHealthUI();
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        agent.ResetPath();
        agent.enabled = false;
        this.enabled = false;

        StartCoroutine(DeathAfter());
        
    }
    IEnumerator DeathAfter()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead || isUsingSkill || Time.time < nextAttackTime) return;

        if (collision.CompareTag("Player"))
        {
            nextAttackTime = Time.time + attackCooldown;
            StartCoroutine(PerformMeleeAttack());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDead || isUsingSkill || Time.time < nextAttackTime) return;

        if (collision.CompareTag("Player"))
        {
            nextAttackTime = Time.time + attackCooldown;
            StartCoroutine(PerformMeleeAttack());
        }
    }

    IEnumerator PerformMeleeAttack()
    {
        isAttacking = true;
        agent.ResetPath();
        animator.SetTrigger("Melee");
        
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }


    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}
