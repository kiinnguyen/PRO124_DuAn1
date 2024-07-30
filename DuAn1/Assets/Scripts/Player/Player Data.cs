using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    private Player player;

    private void Start()
    {
        InitializePlayer();
    }

    private void InitializePlayer()
    {
        player = FindObjectOfType<Player>();
    }
    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("health", player.health);
        PlayerPrefs.SetInt("food", player.food);
        PlayerPrefs.SetInt("gold", player.gold);
    }

    public void LoadPlayerData()
    {
        player.health = PlayerPrefs.GetInt("health", 100);
        player.food = PlayerPrefs.GetInt("food", 100);
        player.gold = PlayerPrefs.GetInt("gold", 0);

        Debug.Log("Upload Player Data Successfully");
    }
}
