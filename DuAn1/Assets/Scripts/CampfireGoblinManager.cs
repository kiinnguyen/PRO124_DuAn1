using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireGoblinManager : MonoBehaviour
{
    public GameObject goblinPrefab;  // Prefab của goblin
    public float spawnRadius = 5f;   // Bán kính xung quanh trại để spawn goblin
    public int goblinCount = 3;     // Số lượng goblin

    private List<Vector2> goblinPositions = new List<Vector2>();

    void Start()
    {
        SpawnGoblins();
    }

    void SpawnGoblins()
    {
        for (int i = 0; i < goblinCount; i++)
        {
            Vector2 spawnPosition;
            do
            {
                spawnPosition = GetRandomPosition();
            }
            while (PositionOverlap(spawnPosition));

            goblinPositions.Add(spawnPosition);
            Instantiate(goblinPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomPosition()
    {
        float angle = Random.Range(0, 2 * Mathf.PI);
        float distance = Random.Range(0, spawnRadius);
        float x = transform.position.x + Mathf.Cos(angle) * distance;
        float y = transform.position.y + Mathf.Sin(angle) * distance;
        return new Vector2(x, y);
    }

    bool PositionOverlap(Vector2 position)
    {
        foreach (Vector2 pos in goblinPositions)
        {
            if (Vector2.Distance(pos, position) < 0.5f) // 0.5f là khoảng cách tối thiểu giữa các goblin
            {
                return true;
            }
        }
        return false;
    }
}
