using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;
    [SerializeField]
    private RectTransform contentPanel;
    [SerializeField]
    private UIInventoryDescription itemDescription;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    public Sprite image;
    public int quantity;
    public string title, description;

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.transform.localScale = Vector3.one;  // Đảm bảo tỷ lệ đúng
            listOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UIInventoryItem obj)
    {
        // Xử lý hành động khi nhấp chuột phải vào item
    }

    private void HandleEndDrag(UIInventoryItem obj)
    {
        // Xử lý khi kết thúc kéo item
    }

    private void HandleSwap(UIInventoryItem obj)
    {
        // Xử lý khi thả item vào vị trí mới
    }

    private void HandleBeginDrag(UIInventoryItem obj)
    {
        // Xử lý khi bắt đầu kéo item
    }

    private void HandleItemSelection(UIInventoryItem obj)
    {
        itemDescription.SetDescription(image, title, description);
        obj.Select();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        if (listOfUIItems.Count > 0)
        {
            listOfUIItems[0].SetData(image, quantity);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
