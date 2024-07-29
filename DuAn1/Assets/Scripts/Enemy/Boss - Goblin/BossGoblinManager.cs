using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGoblinManager : MonoBehaviour
{

    [SerializeField]
    float health;

    private Animator anim;
    private BossGoblinMovement movement;
    void Start()
    {
        anim = GetComponent<Animator>();
        movement = GetComponent<BossGoblinMovement>();

        health = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            movement.isDeadState();
        }
        else
        {
            anim.SetTrigger("Hurt");
        }
    }
}
