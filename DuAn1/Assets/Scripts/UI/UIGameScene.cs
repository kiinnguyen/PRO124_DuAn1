using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameScene : MonoBehaviour
{
    [Header("Classes")]
    [SerializeField] PlayerManager playerManager;


    [Header("Speaker")]
    [SerializeField] AudioSource musicSpeaker;

    [Header("UI Container")]
    [SerializeField] GameObject inventoryBar;
    bool isInventoryBarActive;
    bool isOpenningInventory;

    [SerializeField] Slider healthBar;
    [SerializeField] Text   healthValueText;
    [SerializeField] Text   goldText;
    [SerializeField] Slider foodBar;
    [SerializeField] Text   foodValueText;
    [SerializeField] Slider waterBar;
    [SerializeField] Text   waterValueText;


    [Header("Life Data")]
    [SerializeField] bool startBlood;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();

        if (playerManager != null)
        {
            /*healthBar.value = playerManager.GetHealth();
            foodBar.value = playerManager.GetFood();
            waterBar.value = playerManager.GetWater();
            goldText.text = playerManager.GetGold().ToString();*/

            healthBar.value = 53;
            foodBar.value = 55;
            waterBar.value = 23;
            goldText.text = "10";

        }
        StartCoroutine(LifeCoroutine());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isOpenningInventory)
            StartCoroutine(OpenInventory());
        }
    }




    public void ReturnMenuScene()
    {
        SceneManager.LoadScene(0);

    }


    public void UpdateValueToSlider(string sliderName)
    {
        if (sliderName == "Health")
        {
            healthValueText.text = healthBar.value.ToString();
        }
        if (sliderName == "Food")
        {
            foodValueText.text = foodBar.value.ToString();
        }
        if (sliderName == "Water")
        {
            waterValueText.text = waterBar.value.ToString();
        }
    }

    // Interface 

    IEnumerator LifeCoroutine()
    {
        while (true)
        {


            yield return new WaitForSeconds(4f);

            float number = UnityEngine.Random.Range(2, 5);
            Debug.Log("number:" + number);

            foodBar.value -= number;
            waterBar.value -= number;


            yield return null;
        }
    }

    IEnumerator HealthCoroutine()
    {
        while (startBlood)
        {
            healthBar.value -= 5;

            yield return new WaitForSeconds(5f);
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
}
