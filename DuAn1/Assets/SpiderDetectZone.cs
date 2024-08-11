using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderDetectZone : MonoBehaviour
{
    CaveSpiderManager spiderManager;


    private void Start()
    {
        spiderManager = FindObjectOfType<CaveSpiderManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spiderManager?.StartCombat();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spiderManager?.StopCombat();
        }
    }
}
