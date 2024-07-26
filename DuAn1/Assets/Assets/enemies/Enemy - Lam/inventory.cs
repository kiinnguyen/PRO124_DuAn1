using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{
    // Sử dụng từ điển để lưu trữ các item và số lượng của chúng
    private Dictionary<string, int> items = new Dictionary<string, int>();

    // Hàm để thêm item vào hành trang
    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName]++;
        }
        else
        {
            items[itemName] = 1;
        }

        Debug.Log("Đã thêm item: " + itemName + ". Số lượng hiện tại: " + items[itemName]);
    }

    // Hàm để lấy số lượng item hiện tại
    public int GetItemCount(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            return items[itemName];
        }
        return 0;
    }
}
