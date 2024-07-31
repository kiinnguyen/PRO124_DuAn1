using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : Singleton<PlayerData>
{
    private Player player;

    public void SavePlayerData()
    {
        player = FindObjectOfType<Player>();

        if (player != null)
        {
            PlayerPrefs.SetInt("health", player.health);
            PlayerPrefs.SetInt("food", player.food);
            PlayerPrefs.SetInt("gold", player.gold);
        }

        PlayerPrefs.Save();
    }

    public void LoadPlayerData()
    {
        player = FindObjectOfType<Player>();

        if (player != null)
        { 
            player.health = PlayerPrefs.GetInt("health", 100);
            player.food = PlayerPrefs.GetInt("food", 100);
            player.gold = PlayerPrefs.GetInt("gold", 0);

        }

        Debug.Log("Upload Player Data Successfully");

    }
}
