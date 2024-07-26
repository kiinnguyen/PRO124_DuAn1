using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InOutGate : MonoBehaviour
{
    [SerializeField] GameObject gin;
    [SerializeField] GameObject hin;


    [SerializeField] Transform cave;
    [SerializeField] Transform island;
    private bool canTeleport;
    private string locate;
    private void Start()
    {
        gin = GameObject.Find("Gin");
        hin = GameObject.Find("Hin");

        locate = "island";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canTeleport)
        {
            if (locate == "island")
            {
                locate = "cave";
                gin.transform.position = cave.position;
            }
            else
            {
                locate = "island";
                gin.transform.position = island.position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canTeleport = false;
        }
    }
}
