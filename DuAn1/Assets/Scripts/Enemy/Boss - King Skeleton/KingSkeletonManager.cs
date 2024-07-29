using System.Collections;
using UnityEngine;

public class KingSkeletonManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1000;
    private int currentHealth;
    private Animator animator;
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        // Disable further actions such as movement, attack, etc.
        GetComponent<KingSkeletonMovement>().enabled = false;
    }
}
