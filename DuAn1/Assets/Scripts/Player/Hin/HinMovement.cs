using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class HinMovement : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float attackRange;
    [SerializeField] GameObject meleeArea;

    private NavMeshAgent agent;
    private Animator animator;
    private List<GameObject> enemiesInRange = new List<GameObject>();
    private GameObject currentTargetEnemy;
    private Vector2 lastDirection;
    public bool isDead;


    // inscript

    Vector2 normalizedVelocity;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            Debug.Log("Get NavMesh Done!");
        }

        animator = GetComponent<Animator>();
        player = FindObjectOfType<Player>().transform;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (isDead) { return; }

        if (currentTargetEnemy != null)
        {
            if (Vector2.Distance(transform.position, currentTargetEnemy.transform.position) <= attackRange)
            {
                UpdateAnimator();
                StartCoroutine(Attack());
            }
            else
            {
                MoveToTarget(currentTargetEnemy.transform);
                UpdateAnimator();
            }
        }
        else
        {
            if (enemiesInRange.Count > 0)
            {
                SelectClosestEnemy();
                MoveToTarget(currentTargetEnemy.transform);
                UpdateAnimator();
            }
            else
            {
                MoveToTarget(player);
                UpdateAnimator();
            }
        }
    }

    void MoveToTarget(Transform target)
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Debug.LogError("NavMeshAgent is not active or not on a NavMesh.");
        }
    }

    void UpdateAnimator()
    {
        Vector2 velocity = new Vector2(agent.velocity.x, agent.velocity.y);
        normalizedVelocity = velocity.sqrMagnitude > 0.1f ? velocity.normalized : lastDirection;

        if (velocity.sqrMagnitude > 0.1f)
        {
            lastDirection = normalizedVelocity;
        }

        RotateAttackArea(normalizedVelocity);

        animator.SetFloat("xInput", normalizedVelocity.x);
        animator.SetFloat("yInput", normalizedVelocity.y);
        animator.SetBool("isMoving", velocity.sqrMagnitude > 0.1f);
    }

    private bool isAttacking = false;
    IEnumerator Attack()
    {
        if (isDead) yield break;

        while (currentTargetEnemy != null && Vector2.Distance(transform.position, currentTargetEnemy.transform.position) <= attackRange)
        {
            agent.ResetPath();
            meleeArea.SetActive(true);
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            meleeArea.SetActive(false);

            // Remove enemy if it is dead or out of range
            if (currentTargetEnemy == null || !enemiesInRange.Contains(currentTargetEnemy))
            {
                currentTargetEnemy = null;
                enemiesInRange.Remove(currentTargetEnemy);
                SelectClosestEnemy();
            }
        }

    }

    private void SelectClosestEnemy()
    {
        if (enemiesInRange.Count == 0) return;

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemiesInRange)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        currentTargetEnemy = closestEnemy;
    }

    private void RotateAttackArea(Vector2 vector)
    {
        switch (vector)
        {
            case var v when v == new Vector2(0f, 1f):
                SetRotationAttackArea(90f);
                break;
            case var v when v == new Vector2(0f, -1f):
                SetRotationAttackArea(270f);
                break;
            case var v when v == new Vector2(1f, 0f):
                SetRotationAttackArea(0f);
                break;
            case var v when v == new Vector2(-1f, 0f):
                SetRotationAttackArea(180f);
                break;
            default:
                break;
        }
    }

    private void SetRotationAttackArea(float zValue)
    {
        Vector3 currentRotation = meleeArea.transform.rotation.eulerAngles;
        currentRotation.z = zValue;
        meleeArea.transform.rotation = Quaternion.Euler(currentRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!enemiesInRange.Contains(collision.gameObject))
            {
                enemiesInRange.Add(collision.gameObject);
                if (currentTargetEnemy == null)
                {
                    SelectClosestEnemy();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (enemiesInRange.Contains(collision.gameObject))
            {
                enemiesInRange.Remove(collision.gameObject);
                if (currentTargetEnemy == collision.gameObject)
                {
                    currentTargetEnemy = null;
                    SelectClosestEnemy();
                }
            }
        }
    }
}
