using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Player player;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] Slider foodBar;
    [SerializeField] Slider waterBar;



    string dataPath = "Assets/Data/playerData.json";


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            playerManager = FindObjectOfType<PlayerManager>();
            player = FindObjectOfType<Player>();
            if (playerManager != null)
            {
                healthBar.value = playerManager.GetHealth();
                goldText.text = playerManager.GetGold().ToString();
                foodBar.value = playerManager.GetFood();
                waterBar.value = playerManager.GetWater();
            }
        }
    }

    
    // save data

    public void UpdatePlayerData(int healInput, TextMeshProUGUI goldInput, int foodInput, int waterInput)
    {
        try
        {
            if (int.TryParse(goldInput.text, out int goldValue))
            {
                playerManager.SetPlayerData(healInput, goldValue, foodInput, waterInput);
                Debug.Log("Save Data successfully");
            }
            else
            {
                Debug.LogError("Failed to parse gold input text to int.");
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void SaveData()
    {
        // nếu chưa tồn tại file lưu data ở địa chỉ dataPath thì khởi tạo mới
        // lưu playerManager
        // sử dụng try catch kiểm tra có hoàn thành không
        try
        {
            PlayerManager data = new PlayerManager();

            data.SetPlayerData(
                  playerManager.GetHealth()
                , playerManager.GetGold()
                , playerManager.GetFood()
                , playerManager.GetWater()
                );

            string json = JsonUtility.ToJson(data);
            System.IO.File.WriteAllText(dataPath, json);
            Debug.Log("Data saved successfully.");
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private void GetData()
    {
        // nếu chưa tồn tại file lưu data ở địa chỉ dataPath thì báo lỗi
        // sử dụng playerManager.SetPlayerData(int healthInput, int goldInput, int foodInput, int waterInput) để gán dữ liệu từ data vào playerManager.
        // sử dụng try catch kiểm tra có hoàn thành không
        try
        {
            if (System.IO.File.Exists(dataPath))
            {
                string json = System.IO.File.ReadAllText(dataPath);
                PlayerManager data = JsonUtility.FromJson<PlayerManager>(json);
                data.SetPlayerData(
                        playerManager.GetHealth()
                        , playerManager.GetGold()
                        , playerManager.GetFood()
                        , playerManager.GetWater()
                        );
                Debug.Log("Data loaded successfully.");
            }
            else
            {
                Debug.LogError("Save file not found.");
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }


    // Methods

    public void TakeDamage(int damage)
    {
        if (playerManager.GetHealth() <= 0) // nếu máu player <= 0
        { 
            player.Death(); // chết
        }
        else
        {
            int currentHealth = playerManager.GetHealth(); // lấy máu hiện tại
            currentHealth -= damage; // trừ máu
            playerManager.SetHealth(currentHealth); // cập nhật máu hiện tại
            healthBar.value = playerManager.GetHealth(); // cập nhật máu lên UI
        }
    }

    public void GetGold(int gold, string method)
    {
        int currentGold = playerManager.GetGold(); // lấy tiền hiện tại

        switch (method) // kiểm tra phương thức
        {
            case "Collect":
                currentGold += gold;
                playerManager.SetGold(gold); // cập nhật tiền hiện tại
                break;
            case "Buy":
                currentGold -= gold;
                playerManager.SetGold(gold);
                break;
            default:
                break;
        }

        goldText.text = playerManager.GetGold().ToString(); // cập nhật tiền lên UI
            
    }
}
