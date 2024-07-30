using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossGoblinMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    NavMeshAgent agent;
    SpriteRenderer spriteRenderer;

    [Header("Information")]
    [SerializeField] bool isDead;

    [SerializeField] GameObject player;

    private bool isAttacking = false;
    private bool isUsingSkill = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = FindObjectOfType<Player>().gameObject;

        if (player != null) StartCoroutine(BossGoOn());

        isDead = false;
    }

    IEnumerator BossGoOn()
    {
        while (!isDead)
        {
            if (!isAttacking && !isUsingSkill)
            {
                TargetObject(player.transform);
                UpdateAnimator();
            }
            yield return new WaitForSeconds(1f);
        }

        StopMoving();
        yield return null;
    }

    public void UpdateAnimator()
    {
        float x = transform.position.x - player.transform.position.x;
        spriteRenderer.flipX = x > 0;
    }

    void TargetObject(Transform target)
    {
        if (!isAttacking && !isUsingSkill)
        {
            agent.SetDestination(target.position);
        }
    }

    void StopMoving()
    {
        agent.ResetPath();
    }

    public void SetAttacking(bool attacking)
    {
        isAttacking = attacking;
        if (attacking)
        {
            StopMoving();
        }
    }

    public void SetUsingSkill(bool usingSkill)
    {
        isUsingSkill = usingSkill;
        if (usingSkill)
        {
            StopMoving();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isUsingSkill)
            {
                GetComponent<BossGoblinSkills>().PerformMeleeAttack();
            }
            anim.SetTrigger("Attack 1");
        }
    }

    public void IsDeadState()
    {
        isDead = true;
        StopMoving();
    }
}
