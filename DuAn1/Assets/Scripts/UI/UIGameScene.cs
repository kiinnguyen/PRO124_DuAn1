using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameScene : MonoBehaviour
{
    [Header("Classes")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] Player player;


    [Header("Speaker")]
    [SerializeField] AudioSource musicSpeaker;

    [Header("UI Container")]
    bool isInventoryBarActive;
    bool isOpenningInventory;

    [SerializeField] Slider healthBar;
    [SerializeField] Text   healthValueText;
    [SerializeField] Text   goldText;
    [SerializeField] Slider foodBar;
    [SerializeField] Text   foodValueText;
    [SerializeField] Slider waterBar;
    [SerializeField] Text   waterValueText;

    [SerializeField] Text userName;


    [Header("Life Data")]
    [SerializeField] bool startBlood;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>();
        if (playerManager != null && gameManager != null && player != null)
        {
            healthBar.value = player.health;
            foodBar.value = player.food;
            waterBar.value = player.water;
            goldText.text = player.gold.ToString();
            userName.text = player.userName;
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

    public void UpdateHealthBar(int value)
    {
        healthBar.value = value;
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
        yield return new WaitForSeconds(1f);
        isOpenningInventory = false;
    }
}
