using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public List<GameObject> items; // Danh sách các vật phẩm trong rương
    private bool isOpen = false; // Trạng thái của rương

    void OnMouseDown()
    {
        if (!isOpen)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        isOpen = true;
        Debug.Log("Chest is opened!");

        // Hiển thị các vật phẩm
        foreach (GameObject item in items)
        {
            Instantiate(item, transform.position, Quaternion.identity);
        }
    }
}
