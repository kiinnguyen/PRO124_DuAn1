using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [System.Serializable]
    public class item
    {
        public string itemName;
        public Sprite itemIcon;
        public ItemType itemType;
        public int amount;

        public virtual void Use(Player player)
        {
            // Logic để sử dụng item sẽ được override bởi các class con
        }
    }

    public enum ItemType
    {
        itemHealth,
        itemMana
    }


}
