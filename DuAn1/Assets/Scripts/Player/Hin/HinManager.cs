using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HinManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject player;
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject meleeArea;
    private HinMovement hinMovement;
    private Transform currentTarget;
    private Rigidbody2D rb;
    private List<Collider2D> enemiesInRange = new List<Collider2D>();

    [Header("Combat")]
    public float attackCooldown = 3f;
    private float nextAttackTime = 3f;
    private bool isUsingSkill;
    private bool isDead;
    private bool isPaused;
    public bool isTutorial;

    void Start()
    {
        isTutorial = false;


        player = GameObject.Find("Gin");
        hinMovement = GetComponent<HinMovement>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        meleeArea = transform.Find("Melee Area").gameObject;
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    void Update()
    {
        if (isDead) return;

        if (isTutorial)
        {
            hinMovement.MoveTo(player.transform);
        }
        else
        {
            if (currentTarget != null)
            {
                hinMovement.MoveTo(currentTarget);
            }
            else
            {
                hinMovement.MoveTo(player.transform);
            }

            if (Time.time >= nextAttackTime && currentTarget != null)
            {
                if (Vector2.Distance(transform.position, currentTarget.position) < 2f)
                {
                    StartCoroutine(PerformAttack());
                }
            }
        }

        
    }

    IEnumerator PerformAttack()
    {
        if (currentTarget == null) yield break;

        nextAttackTime = Time.time + attackCooldown;
        agent.ResetPath();
        animator.SetTrigger("Attack");

        // Điều chỉnh hướng nhìn của Hin
        RotateTowardsTarget(currentTarget);

        // Đợi animation tấn công thực hiện xong
        yield return new WaitForSeconds(1f);

        // Kiểm tra va chạm với quái vật trong vùng meleeArea
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(meleeArea.transform.position, meleeArea.GetComponent<BoxCollider2D>().size, 0);
        foreach (Collider2D hitEnemy in hitEnemies)
        {
            if (hitEnemy.CompareTag("Enemy"))
            {
                KnockBack knockBack = hitEnemy.GetComponent<KnockBack>();

                if (knockBack != null)
                {
                    Vector2 knockback = (hitEnemy.transform.position - transform.position).normalized;
                    knockBack.ApplyKnockback(knockback);
                }
                hitEnemy.SendMessage("TakeDamage", 30);
                Debug.Log("Hit Enemy: " + hitEnemy.name);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Add(collision);
            if (currentTarget == null)
            {
                currentTarget = collision.transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(collision);
            if (currentTarget == collision.transform)
            {
                currentTarget = enemiesInRange.Count > 0 ? enemiesInRange[0].transform : null;
            }
        }
    }

    private void RotateTowardsTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        meleeArea.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }


    private void OnEnable()
    {
        GameManager.OnPause.AddListener(HandlePause);
        GameManager.OnResume.AddListener(HandleResume);

    }

    void OnDisable()
    {
        GameManager.OnPause.RemoveListener(HandlePause);
        GameManager.OnResume.RemoveListener(HandleResume);

    }

    void HandlePause()
    {
        isPaused = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isMoving", false);
        Debug.Log("Hin Paused");
    }

    void HandleResume()
    {
        isPaused = false;
        Debug.Log("Hin Resumed");
    }
}
