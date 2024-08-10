using UnityEngine;
using UnityEngine.AI;

public class KingSkeletonMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Player player;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] KingSkeletonManager kingSkeletonManager;

    [Header("Varibles")]
    private bool isChase;
    private Vector2 targetPosition;
    private bool isUsingSkill;
    [SerializeField] bool isPaused;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        kingSkeletonManager = GetComponent<KingSkeletonManager>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
        isChase = false;
        isPaused = false;
    }

    public void StartMove()
    {
        isChase = true;
        InvokeRepeating(nameof(UpdateTargetPosition), 0f, 1f);
    }

    public void StopMove()
    {
        isChase = false;
        CancelInvoke(nameof(UpdateTargetPosition));
        rb.velocity = Vector2.zero;

        animator.SetBool("isMoving", false);
    }

    private void UpdateTargetPosition()
    {
        if (player != null)
        {
            targetPosition = player.transform.position;
        }
    }

    private void Update()
    {
        if (kingSkeletonManager.isDead || isPaused) return;
        if (!isUsingSkill)
        {
            if (isChase)
            {
                agent.SetDestination(targetPosition);
            }
            else
            {
                agent.ResetPath();
            }

            Vector2 direction = (Vector2)agent.destination - rb.position;

            animator.SetFloat("xInput", direction.x);
            animator.SetFloat("yInput", direction.y);
            animator.SetBool("isMoving", agent.velocity.magnitude > 0.1f);
        }
        else
        {
            agent.ResetPath();
        }

    }

    public bool UseSkill(bool state) => isUsingSkill = state;



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

    public void HandlePause()
    {
        isPaused = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("isMoving", false);
    }

    void HandleResume()
    {
        isPaused = false;
    }
}
