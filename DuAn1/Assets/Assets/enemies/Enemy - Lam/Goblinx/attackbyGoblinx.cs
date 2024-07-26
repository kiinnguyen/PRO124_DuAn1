using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackbyGoblinx : MonoBehaviour
{
    public float attackRange = 1.5f; // Phạm vi tấn công của quái vật
    public float attackCooldown = 1f; // Thời gian chờ giữa các đợt tấn công
    private float nextAttackTime = 0f; // Thời gian có thể tấn công tiếp theo
    private Transform player; // Vị trí của người chơi
    private PlayerManager playerManager; // Script quản lý người chơi
    private Animator animator; // Animator của quái vật
    private Player playerHealth;

    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerManager = playerObject.GetComponent<PlayerManager>();
        }
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<Player>();
        //playerManager = player.GetComponent<PlayerManager>();
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
        if (playerManager != null)
        {
            // Tính toán knockBack
            Vector2 knockBackDirection = (player.position - transform.position).normalized;
            float knockBackForce = 4f; // Điều chỉnh độ lớn của lực đẩy tùy theo mong muốn
            Vector2 knockBack = knockBackDirection * knockBackForce;

            // Giảm máu của người chơi và áp dụng knockBack
            playerManager.TakeDamage(10, knockBack);
            Debug.Log("Enemy attacked the player!");
        }


        //if (playerHealth != null)
        //{
        //    playerHealth.TakeDamage(10);
        //    Debug.Log("Enemy attacked the player!");
        //}
        // Giảm máu của người chơi
        //playerHealth.TakeDamage(10);
        //playerManager.TakeDamage(10, knockBack: );
    }
}
