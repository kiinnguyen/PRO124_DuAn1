using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    [SerializeField] Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        StartCoroutine(UnActive());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            KnockBack knockBack = collision.GetComponent<KnockBack>();

            if (knockBack != null)
            {
                Vector2 knockback = (collision.transform.position - transform.position).normalized;
                knockBack.ApplyKnockback(knockback);
            }
            collision.SendMessage("TakeDamage", player.damage);
        }

        if (collision.CompareTag("SpiderWeb"))
        {
            collision.SendMessage("TakeDamage", 5);
        }
    }

    IEnumerator UnActive()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
