using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class KingSkeletonMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [Header("Settings")]
    [SerializeField] private float attackRange = 3.0f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float skillCooldown = 5.0f;

    private GameObject player;
    private bool isAttacking = false;
    private bool isUsingSkill = false;
    private bool isDead = false;

    private Vector2 lastDirection;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = GameObject.Find("Player");

        if (player != null)
        {
            StartCoroutine(BossBehavior());
        }
    }

    IEnumerator BossBehavior()
    {
        while (!isDead || player !=null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange)
            {
                Debug.Log("Attack");
                if (!isUsingSkill && !isAttacking)
                {
                    if (Random.value < 0.3f)
                    {
                        StartCoroutine(Skill());
                    }
                    else
                    {
                        StartCoroutine(MeleeAttack());
                    }
                }
            }
            else
            {
                Debug.Log("Move to plauer");
                agent.SetDestination(player.transform.position);
                animator.SetBool("isMoving", true);
            }
            UpdateAnimator();
            yield return null;
        }
    }

    IEnumerator MeleeAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            agent.ResetPath();
            animator.SetTrigger("Melee");

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                //player.SendMessage("TakeDamage", 30);
            }

            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
        }
    }

    IEnumerator Skill()
    {
        while (!isUsingSkill)
        {
            isUsingSkill = true;
            agent.ResetPath();
            animator.SetTrigger("Attack 1");

            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
            {
                player.SendMessage("TakeDamage", 80);
            }

            yield return new WaitForSeconds(skillCooldown);
            isUsingSkill = false;
        }
    }

    public void UpdateAnimator()
    {
        Vector2 velocity = new Vector2(agent.velocity.x, agent.velocity.y);
        Vector2 normalizedVelocity = velocity.sqrMagnitude > 0.1f ? velocity.normalized : lastDirection;

        if (velocity.sqrMagnitude > 0.1f)
        {
            lastDirection = normalizedVelocity;
        }

        animator.SetFloat("xInput", normalizedVelocity.x);
        animator.SetFloat("yInput", normalizedVelocity.y);
        animator.SetBool("isMoving", velocity.sqrMagnitude > 0.1f);
    }

    public void SetDeadState()
    {
        isDead = true;
        agent.ResetPath();
        animator.SetTrigger("Die");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

        }
    }

}
