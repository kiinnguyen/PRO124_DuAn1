using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private GameManager gameManager;
    private UIGameScene uiGameScene;
    private Player player;
    private string saveFilePath;

    private Animator animator;

    private void Start()
    {
        InitializePlayer();
    }


    public void InitializePlayer()
    {
        animator = GetComponent<Animator>();    
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();
        uiGameScene = FindObjectOfType<UIGameScene>();

        player.health = 100;
        player.food = 88;
        player.water = 77;
        player.gold = 66;
        player.damage = 40;
        if (gameManager != null) 
            player.userName = gameManager.GetUserName();
        player.inventory = new List<Item>();

        Debug.Log("Player Initialized");
    }


    private bool isTakingDamage = false;
    public void TakeDamage(int damage)
    {
        if (isTakingDamage) return;

        isTakingDamage = true;

        int health = player.health - damage;

        if (health <= 0)
        {
            // Die
            animator.SetTrigger("Death");
            uiGameScene.UpdateHealthBar(0);
        }
        else
        {
            animator.SetTrigger("Hurt");
            player.health = health;
            uiGameScene.UpdateHealthBar(player.health);
        }

        StartCoroutine(FinishTakeDamage());
    }

    IEnumerator FinishTakeDamage()
    {
        yield return new WaitForSeconds(1f);

        isTakingDamage = false;

        yield return null;
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

}


[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int health;
    public int food;
    public int water;
    public int damage;
    public List<string> inventory;
}