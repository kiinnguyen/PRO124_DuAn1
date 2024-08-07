using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private GameManager gameManager;
    private UIGameScene uiGameScene;
    private Player player;

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

        if (player != null )
        {
            Debug.Log("Start Load Data For Player");
            PlayerData.Instance.LoadPlayerData();
            transform.position = player.currentPOS;
        }
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
        StartCoroutine(HealingEffect(value));
    }

    IEnumerator HealingEffect(int value)
    {
        float elapsed = 0f;
        float healvalue = value / 10f;
        while (elapsed < 10f)
        {
            elapsed++;
            yield return new WaitForSeconds(1f);
            player.health += (int)healvalue;

            if (player.health >= player.maxHealth)
            {
                player.health = player.maxHealth;
            }
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

    public void SetFood(Slider slider)
    {
        player.food = (int)slider.value;
    }
}
