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
    private Vector3 originalPosition;

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

        originalPosition = transform.position; // Lưu vị trí ban đầu
    }

    void Update()
    {
        if (isDead) return;

        if (isChase)
        {
            if (player != null)
            {
                agent.SetDestination(player.transform.position);
            }
        }
        else
        {
            // Quay về vị trí cũ
            if (Vector3.Distance(transform.position, originalPosition) > 0.2f)
            {
                agent.SetDestination(originalPosition);
            }
            else
            {
                agent.ResetPath();
            }
        }
    }

    public void DeadState(bool state)
    {
        isDead = state;
        isChase = false;
        agent.ResetPath();
        // Thêm logic ngừng mọi hoạt động nếu cần
    }
}
