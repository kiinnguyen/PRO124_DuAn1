using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    [SerializeField] Player player;

    private float damage;
    private void Awake()
    {
        player = FindObjectOfType<Player>();

        damage = player.damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.SendMessage("HurtEnemies", damage);
        }
    }
}
