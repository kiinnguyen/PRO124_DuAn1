using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("Player")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI coinText;


    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();

        if (playerManager != null)
        {
            healthBar.value = playerManager.GetHealth();
            coinText.text = playerManager.GetGold().ToString();
        }
    }

    private void Update()
    {
    }


}
