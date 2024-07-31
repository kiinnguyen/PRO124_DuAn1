using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpammer : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    [SerializeField] private GameObject[] enemyPrefabsList;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private int maxEnemiesToKill = 10;
    [SerializeField] private Transform spawnPos;

    [SerializeField] private int enemiesKilled = 0;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (enemiesKilled < maxEnemiesToKill)
        {
            yield return new WaitForSeconds(spawnInterval);

            int numberOfEnemiesToSpawn = Random.Range(1, 2);
            for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                GameObject enemyPrefab = enemyPrefabsList[Random.Range(0,enemyPrefabsList.Length)];
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPos.position, Quaternion.identity);

                newEnemy.transform.SetParent(transform);

                Enemy enemy = newEnemy.GetComponent<Enemy>();
                if (enemy == null)
                {
                    enemy = newEnemy.AddComponent<Enemy>();
                }
                enemy.OnEnemyDestroyed += HandleEnemyDestroyed;
            }
        }

        Debug.Log("done");
    }

    
    private void HandleEnemyDestroyed()
    {
        enemiesKilled++;
        if (enemiesKilled >= maxEnemiesToKill)
        {
            StopAllCoroutines();
        }
    }
}
