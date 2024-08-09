using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField]
        private UIInventoryPage inventoryUI;

        [SerializeField]
        private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            if (inventoryUI == null)
            {
                Debug.LogError("inventoryUI is not assigned in the Inspector.");
                return;
            }

            if (inventoryData == null)
            {
                Debug.LogError("inventoryData is not assigned in the Inspector.");
                return;
            }

            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            if (inventoryData == null)
            {
                Debug.LogError("inventoryData is not initialized.");
                return;
            }

            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;

            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty) continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            if (inventoryUI == null)
            {
                Debug.LogError("inventoryUI is not initialized.");
                return;
            }

            inventoryUI.ResetAllItems();

            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
        {
            if (inventoryUI == null)
            {
                Debug.LogError("inventoryUI is not assigned.");
                return;
            }

            if (inventoryData == null)
            {
                Debug.LogError("inventoryData is not assigned.");
                return;
            }

            inventoryUI.InitializeInventoryUI(inventoryData.Size);
            inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            inventoryUI.OnSwapItems += HandleSwapItems;
            inventoryUI.OnStartDragging += HandleDragging;
            inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            if (inventoryData == null)
            {
                Debug.LogError("inventoryData is not initialized.");
                return;
            }

            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject);
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }
        }

        private void HandleDragging(int itemIndex)
        {
            if (inventoryData == null)
            {
                Debug.LogError("inventoryData is not initialized.");
                return;
            }

            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty) return;

            if (inventoryUI == null)
            {
                Debug.LogError("inventoryUI is not initialized.");
                return;
            }

            inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            if (inventoryData == null)
            {
                Debug.LogError("inventoryData is not initialized.");
                return;
            }

            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            if (inventoryUI == null)
            {
                Debug.LogError("inventoryUI is not initialized.");
                return;
            }

            if (itemIndex < 0 || itemIndex >= inventoryData.Size)
            {
                Debug.LogWarning($"Item index {itemIndex} is out of range.");
                inventoryUI.ResetSelection();
                return;
            }

            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                inventoryUI.ResetSelection();
                return;
            }

            ItemSO item = inventoryItem.item;
            inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (inventoryUI != null)
                {
                    if (!inventoryUI.isActiveAndEnabled)
                    {
                        inventoryUI.Show();
                        foreach (var item in inventoryData.GetCurrentInventoryState())
                        {
                            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
                        }
                    }
                    else
                    {
                        inventoryUI.Hide();
                    }
                }
                else
                {
                    Debug.LogError("inventoryUI is not initialized.");
                }
            }
        }
    }
}
