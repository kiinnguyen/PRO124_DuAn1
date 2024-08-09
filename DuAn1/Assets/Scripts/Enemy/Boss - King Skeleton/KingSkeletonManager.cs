using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KingSkeletonManager : MonoBehaviour
{
    [Header("Component")]
    public Transform Player; // Đổi tên biến từ player thành Player
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject slimePrefab;
    private KingSkeletonMovement movement;
    [SerializeField] Slider healthBar;

    [Header("Stats")]
    public float health = 1000f;
    private float maxHealth;
    public float attackCooldown = 3f;
    private float nextAttackTime;
    public float skillDuration = 7f;
    private float skillCooldown = 10f;
    private float nextSkillTime;
    private bool isUsingSkill;
    private bool isDead;

    private bool isAttacking = false;
    private bool hasHealed = false; // Cờ kiểm tra hồi máu

    [Header("Information")]
    [SerializeField] int damage;
    [SerializeField] float spawnSlimeInterval = 20f;
    private float nextSpawnTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        Player = FindObjectOfType<Player>().transform; // Đảm bảo Player biến đã được gán
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
        nextSpawnTime = Time.time + spawnSlimeInterval;
        movement = GetComponent<KingSkeletonMovement>();
        if (movement == null)
        {
            Debug.LogError("KingSkeletonMovement component is not attached to the GameObject.");
        }
    }

    void Update()
    {
        if (isDead) return;

        // Spawn slimes every 20 seconds
        if (Time.time >= nextSpawnTime)
        {
            SpawnSlimes();
            nextSpawnTime = Time.time + spawnSlimeInterval;
        }

        // Heal if health is below 40% and not currently using skill
        if (health <= maxHealth * 0.4f && !hasHealed)
        {
            hasHealed = true; // Đặt cờ đã hồi máu
            HealRoutine();
        }

        // Check skill usage
        if (isUsingSkill)
        {
            SkillAttack();
            return;
        }

        if (Time.time >= nextSkillTime)
        {
            StartCoroutine(UseSkill());
        }

        if (movement != null)
        {
            movement.MoveTo(Player); // Di chuyển theo Player
        }
    }

    IEnumerator UseSkill()
    {
        isUsingSkill = true;
        nextSkillTime = Time.time + skillCooldown + skillDuration;
        animator.SetTrigger("Skill1");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        float distance = Vector2.Distance(transform.position, Player.position); // Kiểm tra khoảng cách với Player
        if (distance > 1f && distance < 2f)
        {
            PlayerManager.FindObjectOfType<PlayerManager>().TakeDamage(damage);
        }
        yield return new WaitForSeconds(skillDuration);
        isUsingSkill = false;
    }

    void SkillAttack()
    {
        movement.MoveTo(Player); // Di chuyển theo Player
    }

    void HealRoutine()
    {
        if (isDead) return;

        float healAmount = maxHealth * 0.2f;
        health = Mathf.Min(health + healAmount, maxHealth);
        animator.SetTrigger("Heal");

        SpawnSlimes();

        UpdateHealthUI();
    }

    void SpawnSlimes()
    {
        for (int i = 0; i < 2; i++)
        {
            Instantiate(slimePrefab, transform.position, Quaternion.identity);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        agent.ResetPath();
        animator.SetTrigger("Hurt");
        if (health <= 0)
        {
            health = 0;
            Die();
        }
        UpdateHealthUI();
    }

    void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");
        agent.ResetPath();
        agent.enabled = false;
        this.enabled = false;

        StartCoroutine(DeathAfter());
    }

    IEnumerator DeathAfter()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead || isUsingSkill || Time.time < nextAttackTime) return;

        if (collision.CompareTag("Player"))
        {
            nextAttackTime = Time.time + attackCooldown;
            StartCoroutine(PerformMeleeAttack());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDead || isUsingSkill || Time.time < nextAttackTime) return;

        if (collision.CompareTag("Player"))
        {
            nextAttackTime = Time.time + attackCooldown;
            StartCoroutine(PerformMeleeAttack());
        }
    }

    IEnumerator PerformMeleeAttack()
    {
        isAttacking = true;
        agent.ResetPath();
        animator.SetTrigger("Melee");

        yield return new WaitForSeconds(1f); // Adjust based on your animation duration
        if (Vector2.Distance(transform.position, Player.position) < 1f) // Kiểm tra khoảng cách với Player
        {
            PlayerManager.FindObjectOfType<PlayerManager>().TakeDamage(damage);
        }
        isAttacking = false;
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}
