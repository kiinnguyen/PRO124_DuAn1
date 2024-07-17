using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int health { get; set; }
    int gold { get; set; }
    int food { get; set; }
    int water { get; set; }

    private GameManager gameManager;
    private UIGameScene uiGameScene;


    public void SetPlayerData(int health, int gold, int food, int water)
    {
        this.health = health;
        this.gold = gold;
        this.food = food;
        this.water = water;
    }

    public bool TakeDamage(int damage)
    {
        /*int health = playerData.GetHealth();
        health -= damage;

        if (health <= 0)
        {
            return false;
            // Die
        }
        else
        {
            playerData.SetHealth(health);
            uiGameScene.UpdateHealthBar(playerData.GetHealth());
            return true;
        }*/
        return true;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int healthInput)
    {
        health = healthInput;
    }

    public int GetGold()
    {
        return gold;
    }

    public void SetGold(int goldInput)
    {
        gold = goldInput;
    }

    public int GetFood()
    {
        return food;
    }

    public int GetWater()
    {
        return water;
    }
}

