using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string itemType; // ví dụ: "healing", "damage", "buff"
    public int value;

    public void UseItem(PlayerManager player)
    {
        // Logic sử dụng vật phẩm
        if (itemType == "healing")
        {
            player.Heal(40);
        }
        else if (itemType == "eating")
        {
            
        }
        // Add thêm các loại vật phẩm khác
    }
}
