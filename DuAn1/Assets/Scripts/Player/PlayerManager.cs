using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private GameManager gameManager;
    private UIGameScene uiGameScene;
    private Player player;
    private string saveFilePath;


    private void Start()
    {
        InitializePlayer();
    }


    public void InitializePlayer()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }

        player.health = 100;
        player.food = 100;
        player.water = 100;
        player.gold = 0;
        player.inventory = new List<Item>();

        Debug.Log("Player Initialized");
    }



    public void TakeDamage(int damage)
    {
        int health = player.health - damage;

        if (health <= 0)
        {
            // Die
        }
        else
        {
            player.health = health;
            uiGameScene.UpdateHealthBar(player.health);
        }
    }

    public void Heal(int value)
    {
        int health = player.health + value;

        if (health >= 100) health = 100;
        if (player.health >= 100)
        uiGameScene.UpdateHealthBar(player.health);
    }

    public void Eat(int value)
    {
        if (player.food <= 100)
        {

        }
    }

}


[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int health;
    public int food;
    public int water;
    public int damage;
    public List<string> inventory;
}