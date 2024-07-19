using UnityEngine;
using UnityEngine.AI;
public class HinMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float followDistance = 5f;
    [SerializeField] float attackRange = 5f;
    [SerializeField] float returnToPlayerDistance = 3f;

    private NavMeshAgent agent;
    private Animator animator;
    private GameObject targetEnemy;
    private Vector2 lastDirection;

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
        if (targetEnemy != null && Vector2.Distance(transform.position, targetEnemy.transform.position) <= attackRange)
        {
            MoveToTarget(targetEnemy.transform);
        }
        else if (Vector2.Distance(transform.position, player.position) > returnToPlayerDistance)
        {
            MoveToTarget(player);
        }
        else if (Vector2.Distance(transform.position, player.position) > followDistance)
        {
            MoveToTarget(player);
        }
        else
        {
            StopMovement();
        }

        UpdateAnimator();
    }

    void MoveToTarget(Transform target)
    {
        agent.SetDestination(target.position);
    }

    void StopMovement()
    {
        agent.ResetPath();
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
}
