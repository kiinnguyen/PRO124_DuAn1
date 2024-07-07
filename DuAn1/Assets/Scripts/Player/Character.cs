using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string name;
    public int health;
    public int maxHealth;
    public List<Item> equipment;

    public Character(string name, int maxHealth)
    {
        this.name = name;
        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.equipment = new List<Item>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void EquipItem(Item item)
    {
        equipment.Add(item);
    }

    public void UnequipItem(Item item)
    {
        equipment.Remove(item);
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public int itemID;

    public Item(string name, int id)
    {
        this.itemName = name;
        this.itemID = id;
    }
}
