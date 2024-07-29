using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class GoblinManager : MonoBehaviour
{
    private GoblinMovement goblinMovement;
    private Animator myAnim;
    private int health = 100;

    void Start()
    {
        goblinMovement = GetComponent<GoblinMovement>();
        myAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Attack")
        {
            TakeDamage(20);
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
            goblinMovement.NavAgentWarp(transform.position);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            goblinMovement.isDead = true;
            goblinMovement.StopMoving();
            Die();
        }
        else
        {
            myAnim.SetTrigger("Hurt");
        }
    }

    private void Die()
    {
        myAnim.SetTrigger("Death");
        StartCoroutine(DestroyAfterDeathAnimation());
    }

    private IEnumerator DestroyAfterDeathAnimation()
    {
        yield return new WaitForSeconds(myAnim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
