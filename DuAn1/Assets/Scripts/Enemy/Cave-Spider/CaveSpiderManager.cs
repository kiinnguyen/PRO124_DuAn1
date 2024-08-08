using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSpiderManager : MonoBehaviour
{
    CaveSpdier spider;

    public GameObject webPrefab;
    public Transform webShootPoint;
    public float shootingRange = 10f;

    private GameObject player;
    private Animator anim;

    public bool onAttack = false;


    void Start()
    {
        try
        {

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= shootingRange && !onAttack)
        {

        }
    }

}

[Serializable]

public class CaveSpdier
{
    private int health { get; set; }
    private int damage { get; set; }

    public CaveSpdier(int health, int damage)
    {
        this.health = health;
        this.damage = damage;
    }

    public int Health { get { return health; } }

    public int Damage { get { return damage; } }
}
