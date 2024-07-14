using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackbyenemy : MonoBehaviour
{
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;
    private Transform player;
    //private PlayerHealth playerHealth; // Script quản lý máu của người chơi
    private Animator animator;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //playerHealth = player.GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //if (player != null && playerHealth != null)
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

        animator.SetTrigger("Attack");
        // Giảm máu của người chơi
       // playerHealth.TakeDamage(10);
        Debug.Log("Enemy attacked the player!");
    }
}
