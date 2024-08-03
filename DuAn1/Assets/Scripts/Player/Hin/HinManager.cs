using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HinManager : MonoBehaviour
{
    [Header("Component")]
    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;
    private HinMovement movement;
    [SerializeField] Slider healthBar;

    [Header("Stats")]
    public float health = 500f;
    private float maxHealth;
    public float attackCooldown = 2f;
    private float nextAttackTime;
    public float skillDuration = 5f;
    private float skillCooldown = 15f;
    private float nextSkillTime;
    private bool isUsingSkill;
    private bool isDead;

    private bool isAttacking = false;

    [SerializeField] List<Collider2D> colliderInRange;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        maxHealth = health;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = health;
        }

        nextAttackTime = Time.time;
        nextSkillTime = Time.time + skillCooldown;
        movement = GetComponent<HinMovement>();
    }

    void Update()
    {
        if (isDead) return;

        if (isUsingSkill)
        {
            SkillAttack();
            return;
        }

        if (Time.time >= nextSkillTime)
        {
            StartCoroutine(UseSkill());
        }

        movement.MoveTo(player);
    }

    IEnumerator UseSkill()
    {
        isUsingSkill = true;
        nextSkillTime = Time.time + skillCooldown + skillDuration;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(skillDuration);
        isUsingSkill = false;
    }

    void SkillAttack()
    {
        movement.MoveTo(player);
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        agent.ResetPath();
        animator.SetTrigger("Hurt");
        if (health <= 0)
        {
            Die();
        }
        else
        {
            UpdateHealth(health);
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        agent.ResetPath();
        agent.enabled = false;
        this.enabled = false;
    }

    public void UpdateHealth(float value)
    {
        healthBar.value = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead || isUsingSkill || Time.time < nextAttackTime) return;

        if (collision.CompareTag("Enemy"))
        {
            nextAttackTime = Time.time + attackCooldown;
            StartCoroutine(PerformMeleeAttack());
        }
    }

    IEnumerator PerformMeleeAttack()
    {
        isAttacking = true;
        agent.ResetPath();
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
