using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossGoblinManager : MonoBehaviour
{

    [SerializeField]
    float health;
    [SerializeField]
    Slider healthBar;
    private Animator anim;
    private BossGoblinMovement movement;
    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<BossGoblinMovement>();

        health = 1000;
        healthBar.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            movement.IsDeadState();
        }
        else
        {
            anim.SetTrigger("Hurt");
        }
    }
}
