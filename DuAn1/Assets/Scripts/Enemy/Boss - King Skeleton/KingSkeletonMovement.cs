using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KingSkeletonMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

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
        if (agent != null && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
            UpdateAnimator(agent.velocity);
        }
    }

    private void UpdateAnimator(Vector2 velocity)
    {
        Vector2 normalizedVelocity = velocity.sqrMagnitude > 0.1f ? velocity.normalized : Vector2.zero;
        animator.SetFloat("xInput", normalizedVelocity.x);
        animator.SetFloat("yInput", normalizedVelocity.y);
        animator.SetBool("isMoving", velocity.sqrMagnitude > 0.1f);
    }
}
