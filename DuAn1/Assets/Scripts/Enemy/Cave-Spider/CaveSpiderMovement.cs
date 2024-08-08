using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaveSpiderMovement : MonoBehaviour
{
    public bool isChase;
    public bool isDead;
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    private Animator anim;
    private Player player;
    private CaveSpiderManager spiderManager;

    void Start()
    {
        isChase = false;
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
        if (isDead || isChase) return;


    }

    public void DeadState(bool state)
    {
        isDead = false;
    }
}
