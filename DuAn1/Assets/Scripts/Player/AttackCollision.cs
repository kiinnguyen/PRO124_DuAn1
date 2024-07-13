using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //collision.SendMessage("TakeDamage", playerManager.GetDamage());
            //collision.SendMessage("TakeDamage", 1);
            Destroy(collision.gameObject);
        }
    }
}