using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnockFeedBack : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float strengh = 16, delay = 0.15f;

    [SerializeField]
    UnityEvent OnBegin, OnDone;

    
    public void PlayFeedBack(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;

        rb.AddForce(direction * strengh, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector2.zero;
        OnDone?.Invoke();
    }
}
