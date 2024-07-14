using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    int health { get; set; }
    int gold { get; set; }

    int food { get; set; }

    int water { get; set; }

    public PlayerManager() { }

    public PlayerManager(int health, int gold, int food, int water)
    {
        this.health = health;
        this.gold = gold;
        this.food = food;
        this.water = water;
    }

    public void SetPlayerData(int healthInput, int goldInput, int foodInput, int waterInput)
    {
        health = healthInput;
        gold = goldInput;
        food = foodInput;
        water = waterInput;
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
