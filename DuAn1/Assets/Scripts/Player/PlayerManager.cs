using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    int health { get; set; }
    int gold { get; set; }

    public PlayerManager(int health, int gold)
    {
        this.health = health;
        this.gold = gold;
    }

    public void SetPlayerData(int healthInput, int goldInput)
    {
        health = healthInput;
        gold = goldInput;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetGold()
    {
        return gold;
    }
}
