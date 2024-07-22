using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HinMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float followDistance;
    [SerializeField] float attackRange;
    [SerializeField] float returnToPlayerDistance;

    private NavMeshAgent agent;
    private Animator animator;
    private GameObject targetEnemy;
    private Vector2 lastDirection;

    public bool isDead;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (isDead) { return; }

        if (targetEnemy != null && Vector2.Distance(transform.position, targetEnemy.transform.position) <= attackRange)
        {
            MoveToTarget(targetEnemy.transform);
        }
        if (Vector2.Distance(transform.position, player.position) > returnToPlayerDistance)
        {
            MoveToTarget(player);
        }

        UpdateAnimator();

        if (targetEnemy != null && Vector2.Distance(transform.position, targetEnemy.transform.position) <= 2f)
        {
            StartCoroutine(Attack());
        }
    }

    void MoveToTarget(Transform target)
    {
        agent.SetDestination(target.position);
    }

    void UpdateAnimator()
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

    private bool isAttacking = false;
    IEnumerator Attack()
    {
        if (isDead) yield break;
        if (isAttacking) yield break;

        isAttacking = true;

        while (targetEnemy != null && Vector2.Distance(transform.position, targetEnemy.transform.position) <= 2f)
        {
            agent.ResetPath();
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1.5f);
        }

        isAttacking = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetEnemy = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetEnemy = null;
        }
    }

    public void CheckingLive(Slider slider)
    {
        isDead = slider.value == 0;

        if (isDead) Die();
    }


    private void Die()
    {
        this.enabled = false;
    }
}
