using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectZone : MonoBehaviour
{
    [SerializeField] KingSkeletonMovement kingSkeletonMovement;

    private void Start()
    {
        kingSkeletonMovement = FindObjectOfType<KingSkeletonMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            kingSkeletonMovement.StartMove();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            kingSkeletonMovement.StopMove();
        }
    }
}
