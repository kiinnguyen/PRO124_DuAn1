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
    private Vector2 lastDirection;

    void Start()
    {
        playerPOS = FindObjectOfType<Player>().transform;

        if (playerPOS != null) targetPOS = playerPOS;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        slimeManager = GetComponent<SlimeManager>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (IsDead()) { return; }

        if (targetPOS != null)
        {
            MoveToTarget(targetPOS);
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
        agent.SetDestination(target.position);
    }

    public void StopTarget()
    {
        agent.ResetPath();
    }

    private bool IsDead()
    {
        return slimeManager.isDead;
    }

    private bool isAttacking = false;
    IEnumerator Attack()
    {
        if (IsDead()) yield break;
        if (isAttacking) yield break;

        isAttacking = true;

        while (targetPOS != null && Vector2.Distance(transform.position, targetPOS.transform.position) <= 1f)
        {
            agent.ResetPath();
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(1.5f);
        }

        isAttacking = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(("Player")))
        {
            collision.gameObject.SendMessage("TakeDamage",10);
            StartCoroutine(Attack());
        }
    }

}
