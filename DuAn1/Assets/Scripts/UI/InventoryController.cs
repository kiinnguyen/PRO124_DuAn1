using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    private GameManager gameManager;

    public int inventorySize = 8;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            Debug.Log("Can Get Game Manager");
        }
        else
        {
            Debug.Log("Cant find Game Manager");
        }

        inventoryUI.InitializeInventoryUI(inventorySize);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!inventoryUI.isActiveAndEnabled)
            {
                gameManager.PauseGame();
                inventoryUI.Show();
            }
            else
            {
                gameManager.ResumeGame();
                inventoryUI.Hide();
            }
        }
    }
}
