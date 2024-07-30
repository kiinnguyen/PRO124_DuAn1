using System.Collections;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 direction)
    {
        if (rb != null)
        {
            StopAllCoroutines();
            StartCoroutine(KnockbackCoroutine(direction));
        }
    }

    private IEnumerator KnockbackCoroutine(Vector2 direction)
    {
        float timer = 0;

        while (timer < knockbackDuration)
        {
            rb.velocity = direction * knockbackForce;
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }
}
