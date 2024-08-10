using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChase : MonoBehaviour
{
    [SerializeField] KingSkeletonManager skeletonManager;
    private void Start()
    {
        skeletonManager = FindObjectOfType<KingSkeletonManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skeletonManager?.StartCombat();
        }
    }
}
