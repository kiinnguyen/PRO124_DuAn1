using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameScene : MonoBehaviour
{
    [Header("Speaker")]
    [SerializeField] AudioSource musicSpeaker;

    [Header("UI Container")]
    [SerializeField] GameObject inventoryBar;
    bool isInventoryBarActive;
    bool isOpenningInventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isOpenningInventory)
            StartCoroutine(OpenInventory());
        }
    }

    IEnumerator OpenInventory()
    {
        isOpenningInventory = true;
        isInventoryBarActive = inventoryBar.active ? false : true;
        inventoryBar.SetActive(isInventoryBarActive);
        yield return new WaitForSeconds(1f);
        isOpenningInventory = false;
    }


    public void ReturnMenuScene()
    {
        SceneManager.LoadScene(0);

    }
}
