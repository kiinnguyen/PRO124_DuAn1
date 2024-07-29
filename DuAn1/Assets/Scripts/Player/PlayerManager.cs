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
    private Animator anim;

    private float knockbackForce = 10f;
    private float knockbackDuration = 0.2f;


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
        anim = GetComponent<Animator>();

        player.health = 100;
        player.food = 100;
        player.gold = 0;
        
        player.inventory = new List<Item>();

    }
    private bool isTakingDamage = false;
    public void TakeDamage(int damage)
    {
        if (isTakingDamage) { return; }

        isTakingDamage = true;

        int health = player.health - damage;

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            player.Death();
            uiGameScene.UpdateHealthBar(0);
        }
        else
        {
            anim.SetTrigger("Hurt");
            player.health = health;
            uiGameScene.UpdateHealthBar(player.health);
        }

        isTakingDamage = false;

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
}
