using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowDialog : MonoBehaviour
{
    private bool isPlayerInRange = false;
    [SerializeField] GameObject dialogBanner;

    private DialogText dialogText;

    private List<string> listTalk;


    private void Start()
    {
        listTalk.Add("Hello bạn");
        listTalk.Add("Chúc ngủ ngon");
        listTalk.Add("Ngày mai khỏe nhen");
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            dialogBanner.SetActive(true);
            dialogBanner.GetComponent<DialogText>().AddNewText(listTalk);
        }
    }
}
