using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    private Animator anim;
    private void Start()
    {
        Destroy(this, 5f);

        anim = GetComponent<Animator>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.SendMessage("SlowEffect");
        }
    }

    public void AttackAnim()
    {
        anim.SetTrigger("Attack");
    }

}
