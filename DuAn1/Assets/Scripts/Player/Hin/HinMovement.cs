using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HinMovement : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] Transform playerPOS;
    [SerializeField] NavMeshAgent agent;

    [Header("Information")]
    [SerializeField] Transform targetPOS;
    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 lastDirection;

    [Header("Systems")]
    [SerializeField] bool isPaused;


    void Start()
    {
        playerPOS = FindObjectOfType<Player>().transform;

        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        else
        {
            Debug.Log("Cant get navmesh");
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isPaused) return;
        if (targetPOS != null && !isPaused)
        {
            MoveTo(targetPOS);
        }
        else
        {
            MoveTo(playerPOS);
        }

        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        Vector2 velocity = new Vector2(agent.velocity.x, agent.velocity.y);
        Vector2 normalizedVelocity = velocity.sqrMagnitude > 0.1f ? velocity.normalized : lastDirection;

        if (velocity.sqrMagnitude > 0.1f)
        {
            lastDirection = normalizedVelocity;
        }

        animator.SetFloat("xInput", normalizedVelocity.x);
        animator.SetFloat("yInput", normalizedVelocity.y);
        animator.SetBool("isMoving", velocity.sqrMagnitude > 0.1f);
    }

    public void MoveTo(Transform target)
    {
        if (agent == null || !agent.isOnNavMesh) return;

        try
        {
            agent.SetDestination(target.position);
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning("Failed to set destination: " + ex.Message);
        }
    }

    private void OnEnable()
    {
        GameManager.OnPause.AddListener(HandlePause);
        GameManager.OnResume.AddListener(HandleResume);
    }

    private void OnDisable()
    {
        GameManager.OnPause.RemoveListener(HandlePause);
        GameManager.OnResume.RemoveListener(HandleResume);
    }

    void HandlePause()
    {
        isPaused = true;
        agent.ResetPath();
        animator.SetBool("isMoving", false);
        Debug.Log("Hin Paused");
    }

    void HandleResume()
    {
        isPaused = false;
        Debug.Log("Hin Resumed");
    }

}
