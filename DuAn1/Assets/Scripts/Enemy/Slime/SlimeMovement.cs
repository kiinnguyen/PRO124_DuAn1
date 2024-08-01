using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Transform playerPOS;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] SlimeManager slimeManager;

    [Header("Information")]
    [SerializeField] Transform targetPOS;
    [SerializeField] float damage = 10f;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 lastDirection;

    [Header("Player")]
    [SerializeField] Player player;

    [Header("Systems")]
    [SerializeField] bool isPaused;
    private bool isAttacking = false;

    void Start()
    {
        player = FindObjectOfType<Player>();

        if (player != null)
        {
            playerPOS = player.transform;
            targetPOS = playerPOS;
        }

        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        else
        {
            Debug.Log("Cant get navmesh");
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        slimeManager = GetComponent<SlimeManager>();
    }

    void Update()
    {
        if (IsDead()) return;

        if (player.isDeadState())
        {
            agent.ResetPath();
            return;
        }

        if (targetPOS != null && !isPaused)
        {
            MoveToTarget(targetPOS);
        }
        else
        {
            if (agent.hasPath)
            {
                agent.ResetPath();
            }
        }

        UpdateAnimator();
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

    void MoveToTarget(Transform target)
    {
        if (agent != null && agent.isOnNavMesh)
        {
            try
            {
                agent.SetDestination(target.position);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("Failed to set destination: " + ex.Message);
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is not properly initialized or not on a NavMesh.");
        }
    }

    public void StopTarget()
    {
        agent.ResetPath();
    }

    private bool IsDead()
    {
        return slimeManager.isDead;
    }

    IEnumerator Attack()
    {
        if (IsDead() || isAttacking) yield break;

        isAttacking = true;

        while (targetPOS != null && Vector2.Distance(transform.position, targetPOS.position) <= 1f)
        {
            if (agent.isOnNavMesh)
            {
                agent.ResetPath();
            }
            yield return new WaitForSeconds(1.5f);
        }

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Attack by Slime");
            KnockBack knockBack = collision.GetComponent<KnockBack>();

            if (knockBack != null)
            {
                Vector2 knockback = (collision.transform.position - transform.position).normalized;
                knockBack.ApplyKnockback(knockback);
                collision.SendMessage("TakeDamage", 10);
                StartCoroutine(Attack());
            }
        }
    }

    // Pause - Resume
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
        Debug.Log("Slime Paused");
    }

    void HandleResume()
    {
        isPaused = false;
        Debug.Log("Slime Resumed");
    }
}
