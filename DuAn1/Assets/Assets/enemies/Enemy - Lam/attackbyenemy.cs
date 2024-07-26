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
    private float knockBackForce = 4f; // Độ lớn của lực đẩy

    void Start()
    {
        // Tìm người chơi bằng tag và lấy các tham chiếu
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerManager = playerObject.GetComponent<PlayerManager>();

            if (playerManager == null)
            {
                Debug.LogError("Không tìm thấy PlayerManager trên đối tượng người chơi!");
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng người chơi với tag 'Player'!");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Không tìm thấy Animator trên đối tượng quái vật!");
        }
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
        if (animator != null)
        {
            // Kích hoạt animation tấn công
            animator.SetTrigger("Attack");
        }

        if (playerManager != null)
        {
            // Tính toán knockback
            Vector2 knockBackDirection = (player.position - transform.position).normalized;
            Vector2 knockBack = knockBackDirection * knockBackForce;

            // Giảm máu của người chơi và áp dụng knockback
            playerManager.TakeDamage(10, knockBack);
            Debug.Log("Quái vật đã tấn công người chơi!");
        }
    }

}
