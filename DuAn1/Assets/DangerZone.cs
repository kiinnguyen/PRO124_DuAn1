using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(DangerZoneCoroutine());
    }

    IEnumerator DangerZoneCoroutine()
    {
        float elapsed = 0f;
        float duration = 2f; // Thời gian để mờ dần

        Color originalColor = spriteRenderer.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration); // Giảm giá trị alpha từ 1 về 0
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha); // Cập nhật màu với alpha mới
            yield return null; // Đợi frame tiếp theo
        }

        Destroy(gameObject); // Hủy đối tượng sau khi mờ dần
    }
}
