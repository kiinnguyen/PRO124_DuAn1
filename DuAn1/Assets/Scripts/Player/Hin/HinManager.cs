using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HinManager : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private Slider healthBar;

    [SerializeField] private bool isHurting = false;

    public bool isDead;

    private Animator anim;

    private bool isCounting;

    void Start()
    {
        anim = GetComponent<Animator>(); 


        health = 100;
        if (healthBar != null) healthBar.value = health;

        StartCoroutine(HealAfterCombat());
    }

    private void Update()
    {
        if (isDead) return;
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isHurting) return;
        isHurting = true;
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Die");
            anim.SetTrigger("Death");
        }
        else StartCoroutine(GetDamage());
        
        healthBar.value = health <= 0 ? 0 : health;
    }

    IEnumerator GetDamage()
    {
        anim.SetTrigger("Hurt");

        yield return new WaitForSeconds(0.5f);

        isHurting = false;

        yield return null;
    }

    public void CheckingLive(Slider slider)
    {
        isDead = slider.value == 0;
    }

    IEnumerator HealAfterCombat()
    {
        while (true)
        {
            isCounting = true;

            float timer = 0;

            while (timer < 5f)
            {
                if (isHurting)
                {
                    timer = 0f;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            health += 5;
            if (health >= 100)
            {
                healthBar.value = 100;
            }
            else healthBar.value = health;
            isCounting = false;
        }
    }
}
