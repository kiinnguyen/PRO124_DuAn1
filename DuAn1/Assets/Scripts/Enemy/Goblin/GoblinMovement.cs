using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinMovement : MonoBehaviour
{
    [Header("Component")]
    private Animator myAnim;
    private Transform target;
    public Transform homePosition;
    private NavMeshAgent navAgent;
    [SerializeField] Rigidbody2D rb;

    [SerializeField]
    private float maxRange = 5f;
    [SerializeField]
    private float minRange = 1f;


    private Vector2 lastDirection;

    public bool isDead = false;
    [SerializeField] bool isPaused;
    private float distanceToTarget;



    void Start()
    {
        isPaused = false;


        rb = GetComponent<Rigidbody2D>(); 
        myAnim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<Player>().transform;
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;
    }

    void Update()
    {
        if (isDead || isPaused) return;
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= maxRange && distanceToTarget >= minRange)
        {
            navAgent.SetDestination(target.position);
        }
        else if (distanceToTarget > maxRange)
        {
            navAgent.ResetPath();
        }
        UpdateAnimator();
    }

    public void UpdateAnimator()
    {
        Vector2 velocity = new Vector2(navAgent.velocity.x, navAgent.velocity.y);
        Vector2 normalizedVelocity = velocity.sqrMagnitude > 0.1f ? velocity.normalized : lastDirection;

        if (velocity.sqrMagnitude > 0.1f)
        {
            lastDirection = normalizedVelocity;
        }

        myAnim.SetFloat("moveX", normalizedVelocity.x);
        myAnim.SetFloat("moveY", normalizedVelocity.y);
        myAnim.SetBool("isMoving", velocity.sqrMagnitude > 0.1f);
    }

    public void GoHome()
    {
        navAgent.SetDestination(homePosition.position);

        Vector3 direction = homePosition.position - transform.position;
        myAnim.SetFloat("moveX", direction.x);
        myAnim.SetFloat("moveY", direction.y);

        if (Vector3.Distance(transform.position, homePosition.position) < 0.1f)
        {
            myAnim.SetBool("isMoving", false);
        }
    }

    public void NavAgentWarp(Vector3 position)
    {
        navAgent.Warp(position);
    }
    
    public void StopMoving()
    {
        navAgent.ResetPath();
        target = null;
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

            myAnim.SetTrigger("Attack");

            yield return new WaitForSeconds(myAnim.GetCurrentAnimatorStateInfo(0).length);

            isAttacking = false;

            yield return null;
        }
    }


    private void OnEnable()
    {
        GameManager.OnPause.AddListener(HandlePause);
        GameManager.OnResume.AddListener(HandleResume);

    }

    void OnDisable()
    {
        GameManager.OnPause.RemoveListener(HandlePause);
        GameManager.OnResume.RemoveListener(HandleResume);

    }

    void HandlePause()
    {
        isPaused = true;
        rb.velocity = Vector2.zero;
        navAgent.ResetPath();
        myAnim.SetBool("isMoving", false);
        Debug.Log("Goblin Paused");
    }

    void HandleResume()
    {
        isPaused = false;
        Debug.Log("Goblin Resumed");
    }
}
