using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potion : MonoBehaviour
{
    public string itemName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CollectItem(other.gameObject);
        }
    }

    private void CollectItem(GameObject player)
    {
        // Lấy component Inventory từ đối tượng người chơi
        //Inventory playerInventory = player.GetComponent<Inventory>();

        //if (playerInventory != null)
        //{
        //    playerInventory.AddItem(itemName); // Thêm item vào hành trang của người chơi
        //}

        //Debug.Log("Item đã được thu thập: " + itemName);
        //Destroy(gameObject); // Hủy đối tượng item sau khi thu thập
    }
}
