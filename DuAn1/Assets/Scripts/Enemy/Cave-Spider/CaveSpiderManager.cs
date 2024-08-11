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

    private CaveSpiderMovement spiderMovement; // Thêm tham chiếu tới CaveSpiderMovement

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
            spiderMovement.isChase = true; // Bắt đầu di chuyển theo người chơi
            StartCoroutine(SpiderCombat());
        }
    }

    public void StopCombat()
    {
        StopAllCoroutines();
        onAttack = false;
        spiderMovement.isChase = false; // Dừng di chuyển và quay lại vị trí ban đầu
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
            yield return new WaitForSeconds(5f); // Kiểm tra khoảng cách mỗi giây
        }
    }

    IEnumerator ShootWebSpider()
    {
        Vector2 pos = player.transform.position;

        Instantiate(dangerRange, pos, Quaternion.identity);

        yield return new WaitForSeconds(1.9f);

        Instantiate(webPrefab, pos, Quaternion.identity);

        yield return new WaitForSeconds(5f);
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
        // hiệu ứng nhấp nháy đỏ khi bị đánh trúng
    }

    private void Die()
    {
        anim.SetTrigger("Die");
        spiderMovement.DeadState(true); // Báo cho CaveSpiderMovement rằng nhện đã chết
        StopCombat();
        // Thêm logic hủy hoặc respawn ở đây nếu cần
    }
}
