using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HinMovement : MonoBehaviour
{
    [Header("Components")]
    public NavMeshAgent agent;
    public Animator animator;

    private Vector2 lastDirection;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    public void MoveTo(Transform target)
    {
        if (target == null || agent == null || !agent.isOnNavMesh) return;

        agent.SetDestination(target.position);
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        Vector2 velocity = new Vector2(agent.velocity.x, agent.velocity.y);
        Vector2 normalizedVelocity = velocity.sqrMagnitude > 0.1f ? velocity.normalized : lastDirection;

        float xInput = Mathf.Round(normalizedVelocity.x);
        float yInput = Mathf.Round(normalizedVelocity.y);

        if (velocity.sqrMagnitude > 0.1f)
        {
            lastDirection = normalizedVelocity;
        }

        animator.SetFloat("xInput", xInput);
        animator.SetFloat("yInput", yInput);
        animator.SetBool("isMoving", velocity.sqrMagnitude > 0.1f);
    }
}
