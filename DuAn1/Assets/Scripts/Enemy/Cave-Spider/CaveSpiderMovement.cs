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
    private CaveSpiderManager spiderManager;

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
        spiderManager = GetComponent<CaveSpiderManager>();
    }

    void Update()
    {
        if (spiderManager.onAttack)
        {
            agent.ResetPath();
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
            return;
        }

        if (player != null)
        {
            agent.SetDestination(player.transform.position);
        }

        anim.SetFloat("xInput", agent.velocity.x);
        anim.SetFloat("yInput", agent.velocity.y);
        anim.SetBool("isMoving", rb.velocity.magnitude > 0.1f);
    }
}
