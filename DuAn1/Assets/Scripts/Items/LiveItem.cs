using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveItem : Item
{
    public int healAmount;

    private void Start()
    {
    }

    public override void Use()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Use();
        }
    }
}
