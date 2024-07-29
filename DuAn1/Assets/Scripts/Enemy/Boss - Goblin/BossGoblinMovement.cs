using System.Collections;
using System.Collections.Generic;
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

    private Vector2 lastDirection;

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
            TargetObject(player.transform);
            UpdateAnimator();
            yield return new WaitForSeconds(1f);
        }

        StopMoving();
        yield return null;
    }

    public void UpdateAnimator()
    {
        float x = transform.position.x - player.transform.position.x;
        spriteRenderer.flipX =  x > 0 ? true : false;
    }

    void TargetObject(Transform gameobject)
    {
        agent.SetDestination(gameobject.position);
    }

    void StopMoving()
    {
        agent.ResetPath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Attack());
        }
    }
    bool isAttacking = false;
    IEnumerator Attack()
    {
        if (isAttacking) yield return null;
        else
        {
            isAttacking = true;

            anim.SetTrigger("Attack");

            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

            isAttacking = false;

            yield return null;
        }
    }


    public void isDeadState()
    {
        isDead = true;
    }
}
