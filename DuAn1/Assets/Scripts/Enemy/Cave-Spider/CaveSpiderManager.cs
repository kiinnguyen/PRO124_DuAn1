using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSpiderManager : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int damage;

    public GameObject webPrefab;
    public GameObject dangerRange;
    public Transform webShootPoint;
    public float shootingRange = 10f;

    private Player player;
    private Animator anim;

    public bool onAttack = false;

    private CaveSpiderMovement spiderMovement;

    void Start()
    {
        try
        {
            anim = GetComponent<Animator>();
            player = FindObjectOfType<Player>();
            spiderMovement = GetComponent<CaveSpiderMovement>();

            StopCombat();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void StartCombat()
    {
        if (!onAttack)
        {
            onAttack = true;
            spiderMovement.isChase = true;
            StartCoroutine(SpiderCombat());
        }
    }

    public void StopCombat()
    {
        StopAllCoroutines();
        onAttack = false;
        spiderMovement.isChase = false;
    }

    IEnumerator SpiderCombat()
    {
        while (onAttack)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= shootingRange)
            {
                StartCoroutine(ShootWebSpider());
            }
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator ShootWebSpider()
    {
        // Kiểm tra khoảng cách lần nữa trước khi bắn
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= shootingRange)
        {
            Vector2 pos = player.transform.position;

            Instantiate(dangerRange, pos, Quaternion.identity);

            yield return new WaitForSeconds(1.9f);

            Instantiate(webPrefab, pos, Quaternion.identity);

            yield return new WaitForSeconds(5f);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(HurtCoroutine());
        }
    }

    IEnumerator HurtCoroutine()
    {
        // Giả sử bạn có SpriteRenderer gắn trên nhện
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color originalColor = sr.color;

        // Làm nhện nhấp nháy đỏ
        for (int i = 0; i < 3; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = originalColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void Die()
    {
        anim.SetTrigger("Die"); 
        spiderMovement.DeadState(true); // Báo cho CaveSpiderMovement rằng nhện đã chết
        StopAllCoroutines();
        StopCombat();
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Attack by Spider");
            KnockBack knockBack = collision.gameObject.GetComponent<KnockBack>();

            if (knockBack != null)
            {
                Vector2 knockback = (collision.transform.position - transform.position).normalized;
                knockBack.ApplyKnockback(knockback);
                collision.gameObject.SendMessage("TakeDamage", damage);
            }
        }
    }
}
