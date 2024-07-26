using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemies : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 3.0f; // Tốc độ di chuyển của kẻ địch
    public float detectionRange = 10.0f; // Phạm vi phát hiện người chơi
    public float attackRange = 1.5f; // Phạm vi tấn công
    public int health = 100; // Máu của kẻ địch
    private Animator animator;

    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < detectionRange)
        {
            // Nếu người chơi trong phạm vi phát hiện, di chuyển về phía người chơi
            MoveTowardsPlayer(distanceToPlayer);
            
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
    void MoveTowardsPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > attackRange)
        {
            // Di chuyển về phía người chơi nếu không trong phạm vi tấn công
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            animator.SetBool("isMoving", true);
            animator.SetBool("isAttacking", false);
        }
        else
        {
            // Nếu trong phạm vi tấn công, tấn công người chơi
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        animator.SetBool("isAttacking", true);
        animator.SetBool("isMoving", false);

        // Logic tấn công người chơi
        Debug.Log("Attacking Player");
        // Bạn có thể thêm logic giảm máu của người chơi ở đây
    }

    public void TakeDamage(int damage)
    {
        // Logic nhận sát thương
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        animator.SetTrigger("Die");
        // Logic khi kẻ địch chết
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }
}
