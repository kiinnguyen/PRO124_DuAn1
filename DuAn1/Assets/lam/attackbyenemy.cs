using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackbyenemy : MonoBehaviour
{
    public float attackRange = 1.5f; // Phạm vi tấn công của quái vật
    public float attackCooldown = 1f; // Thời gian chờ giữa các đợt tấn công
    private float nextAttackTime = 0f; // Thời gian có thể tấn công tiếp theo
    private Transform player; // Vị trí của người chơi
    private PlayerManager playerManager; // Script quản lý người chơi
    private Animator animator; // Animator của quái vật

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerManager = player.GetComponent<PlayerManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null && playerManager != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange && Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void Attack()
    {
        // Kích hoạt animation tấn công
        animator.SetTrigger("Attack");

        // Giảm máu của người chơi
        playerManager.TakeDamage(10); // Giảm 10 máu của người chơi
        Debug.Log("Enemy attacked the player!");
    }
}
