using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    public float slowDuration = 2.0f; 
    public float slowMultiplier = 0.5f; 
    void Start()
    {
        currentHealth = maxHealth;
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
        Destroy(gameObject);
    }

    //void OnCollisionEnter(Collision collision)
    //{
        //if (collision.gameObject.tag == "Player") 
        //{
            //StartCoroutine(SlowPlayer(collision.gameObject));
        //}
    //}

    //private IEnumerator SlowPlayer(GameObject player)
    //{
        //Player playerMovement = player.GetComponent<Player>();

        //if (playerMovement != null)
        //{
            //float originalSpeed = playerMovement.speedmove;
            //playerMovement.speedmove *= slowMultiplier; 

            //yield return new WaitForSeconds(slowDuration);
            //playerMovement.speedmove = originalSpeed; 
        //}
    //}
}