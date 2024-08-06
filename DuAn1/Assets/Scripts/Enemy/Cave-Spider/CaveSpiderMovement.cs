using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaveSpiderMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    private Animator anim;

    private Player player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        anim.SetBool("isMoving", rb.velocity.magnitude > 0.1f);

        agent.SetDestination(player.transform.position);
    }
}
