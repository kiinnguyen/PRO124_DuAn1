using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int maxHealth = 100; // Máu tối đa
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth; // Khởi tạo máu hiện tại
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Thêm hiệu ứng phá hủy nếu cần thiết, ví dụ như chơi âm thanh, animation v.v.
        Destroy(gameObject); // Phá hủy game object
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") // Kiểm tra nếu va chạm với đạn
        {
            TakeDamage(10); // Gây sát thương 10
        }

    }
}