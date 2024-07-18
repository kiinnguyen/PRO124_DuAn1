using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<GameObject> items;
    private bool isOpen = false;

    [SerializeField] GameObject pressDialog;

    [SerializeField] GameObject chestBanner;

    private void Start()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressDialog.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Open chest");
            chestBanner.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pressDialog.SetActive(false);
        }
    }
}
