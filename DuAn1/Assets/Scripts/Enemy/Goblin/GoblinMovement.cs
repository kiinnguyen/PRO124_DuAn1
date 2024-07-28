using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinMovement : MonoBehaviour
{
    private Animator myAnim;
    private Transform target;
    public Transform homePosition;
    private NavMeshAgent navAgent;

    [SerializeField]
    private float maxRange = 10f;
    [SerializeField]
    private float minRange = 1f;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = FindAnyObjectByType<Player>().transform;
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= maxRange && distanceToTarget >= minRange)
        {
            FollowPlayer();
        }
        else if (distanceToTarget > maxRange)
        {
            GoHome();
        }
    }

    public void FollowPlayer()
    {
        myAnim.SetBool("isMoving", true);
        navAgent.SetDestination(target.position);

        Vector3 direction = target.position - transform.position;
        myAnim.SetFloat("moveX", direction.x);
        myAnim.SetFloat("moveY", direction.y);
    }

    public void GoHome()
    {
        navAgent.SetDestination(homePosition.position);

        Vector3 direction = homePosition.position - transform.position;
        myAnim.SetFloat("moveX", direction.x);
        myAnim.SetFloat("moveY", direction.y);

        if (Vector3.Distance(transform.position, homePosition.position) < 0.1f)
        {
            myAnim.SetBool("isMoving", false);
        }
    }

    public void NavAgentWarp(Vector3 position)
    {
        navAgent.Warp(position);
    }
}
