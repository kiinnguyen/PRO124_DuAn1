using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
    private UIGameScene uiGameScene;
    private Player player;
    private string saveFilePath;

    private Rigidbody2D rb;


    private void Start()
    {
        InitializePlayer();
    }

    public void InitializePlayer()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        uiGameScene = FindObjectOfType<UIGameScene>();
        rb = GetComponent<Rigidbody2D>();

        player.health = 100;
        player.food = 100;
        player.water = 100;
        player.gold = 100;
        if (gameManager != null)
        {
            player.userName = gameManager.GetUserName();
        }       
        player.inventory = new List<Item>();

        Debug.Log("Player Initialized");
    }


    private void OnEnable()
    {
        GameManager.Instance.OnPause += HandlePause;
        GameManager.Instance.OnResume += HandleResume;
    }

    private void OnDisable()
    {   
        GameManager.Instance.OnPause -= HandlePause;
        GameManager.Instance.OnResume -= HandleResume;
    }

    private void HandlePause()
    {
        // Logic to handle pause, e.g., disable components or stop animations
        Debug.Log("Game Paused - Handling in SomeGameComponent");
    }

    private void HandleResume()
    {
        // Logic to handle resume, e.g., enable components or start animations
        Debug.Log("Game Resumed - Handling in SomeGameComponent");
    }


    public void TakeDamage(int damage, Vector2 knockBack)
    {
        int health = player.health - damage;    

        if (health <= 0)
        {
            // Die
            uiGameScene.UpdateHealthBar(0);
        }
        else
        {
            player.health = health;
            uiGameScene.UpdateHealthBar(player.health);
            rb.AddForce(knockBack, ForceMode2D.Impulse);
        }
      
    }

    public void Heal(int value)
    {
        int health = player.health + value;

        if (health >= 100) health = 100;
        else
        {
            player.health = health;
            uiGameScene.UpdateHealthBar(player.health);
        }
    }

    public void Eat(int value)
    {
        if (player.food <= 100)
        {

        }
    }

    public void UpdateHealth(Text value)
    {
        int newValue = int.Parse(value.text);

        player.health = newValue;
    }

    public void UpdateFood(Text value)
    {
        int newValue = int.Parse(value.text);

        player.food = newValue;
    }
    public void UpdateWater(Text value)
    {
        int newValue = int.Parse(value.text);

        player.water = newValue;
    }
}
