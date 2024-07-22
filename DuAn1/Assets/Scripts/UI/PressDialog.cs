using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressDialog : MonoBehaviour
{
    public float amplitude = 5f; // Biên độ di chuyển
    public float speed = 1f; // Tốc độ di chuyển

    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {

        while (true)
        {
            float newY = transform.position.y + Mathf.Sin(Time.time * speed) * amplitude;
            transform.position = new Vector2(startPosition.x, newY);

            yield return null; // Chờ frame tiếp theo
        }
    }
}
