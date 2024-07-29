using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossGoblinMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    NavMeshAgent agent;

    [Header("Information")]
    [SerializeField] bool isDead;

    [SerializeField] GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.updatePosition = false;
        agent.updateUpAxis = false;

        player = GameObject.Find("Player");

        if (player != null) StartCoroutine(BossGoOn());

        isDead = false;
    }

    IEnumerator BossGoOn()
    {
        TargetObject(player.transform);
        while (!isDead)
        {
            yield return null;
        }

        StopMoving();
        
    }

    void TargetObject(Transform gameobject)
    {
        agent.SetDestination(gameobject.position);
    }

    void StopMoving()
    {
        agent.ResetPath();
    }
}
