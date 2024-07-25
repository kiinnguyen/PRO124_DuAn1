using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bossMovement : MonoBehaviour
{
    public Transform player; // Người chơi
    private NavMeshAgent agent;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            // Di chuyển boss đến vị trí của người chơi
            agent.SetDestination(player.position);

            // Cập nhật animation dựa trên tốc độ di chuyển của NavMeshAgent
            UpdateAnimation();
        }
    }

    void UpdateAnimation()
    {
        // Cập nhật trạng thái animation dựa trên tốc độ di chuyển của NavMeshAgent
        float speed = agent.velocity.magnitude;
        animator.SetFloat("Speed", speed);
    }
}
