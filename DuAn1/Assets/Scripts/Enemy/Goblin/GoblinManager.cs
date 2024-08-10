using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinManager : MonoBehaviour
{
    private GoblinMovement goblinMovement;
    private GameSceneData gameSceneData;
    private Animator myAnim;

    public int id;
    public int health;
    public Vector3 POS;

    [Header("Items when drop")]
    [SerializeField] List<GameObject> listObject;
    [SerializeField] private float percentToDropItem;

    [Header("Campfire Area Settings")]
    [SerializeField] private Transform campfirePosition; // Vị trí lửa trại
    [SerializeField] private float spawnRadius = 5f;     // Bán kính spawn goblin quanh lửa trại
    [SerializeField] private float minSpawnDistance = 1f; // Khoảng cách tối thiểu từ lửa trại

    void Start()
    {
        SetRandomPositionAroundCampfire(); // Thiết lập vị trí spawn ngẫu nhiên quanh lửa trại

        transform.position = POS;

        gameSceneData = FindObjectOfType<GameSceneData>();
        goblinMovement = GetComponent<GoblinMovement>();
        myAnim = GetComponent<Animator>();
    }

    private void SetRandomPositionAroundCampfire()
    {
        Vector3 randomPosition;

        // Lặp lại cho đến khi tìm được vị trí hợp lệ trong khu vực spawn
        do
        {
            Vector2 randomPoint = UnityEngine.Random.insideUnitCircle * spawnRadius;
            randomPosition = new Vector3(
                campfirePosition.position.x + randomPoint.x,
                campfirePosition.position.y + randomPoint.y,
                campfirePosition.position.z
            );
        } while (Vector3.Distance(randomPosition, campfirePosition.position) < minSpawnDistance);

        POS = randomPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KnockBack knockBack = collision.GetComponent<KnockBack>();

            if (knockBack != null)
            {
                Vector2 knockback = (collision.transform.position - transform.position).normalized;
                knockBack.ApplyKnockback(knockback);
                collision.SendMessage("TakeDamage", 20);
            }
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

    private void OnDestroy()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 1f);

        if (randomNumber <= percentToDropItem)
        {
            if (listObject.Count > 0)
            {
                GameObject newObject = listObject[UnityEngine.Random.Range(0, listObject.Count)];
                Instantiate(newObject, transform.position, Quaternion.identity);
            }
        }
    }
}
