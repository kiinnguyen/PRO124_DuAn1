using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveItem : Item
{
    public int healAmount;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }

    public override void Use()
    {
        Debug.Log("Heal: " + itemName);
        switch (itemType)
        {   
            case "Health":
                playerManager.Heal(healAmount);
                break;
            case "Food":
                break;
            case "Water":
                break;
            default:
                break;
        }
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Use();
        }
    }
}
