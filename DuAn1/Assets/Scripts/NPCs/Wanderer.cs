using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This makes an object move randomly in a set of directions, with some random time delay in between each decision.
/// </summary>
public class Wanderer : MonoBehaviour
{
    private Transform thisTransform;
    private Rigidbody2D rb;
    private Animator animator;

    [Header("Movement Settings")]
    public float moveSpeed = 0.2f;
    public Vector2 decisionTime = new Vector2(1, 4);

    private float decisionTimeCount = 0;
    private Vector2[] moveDirections = new Vector2[] { Vector2.right, Vector2.left, Vector2.up, Vector2.down, Vector2.zero, Vector2.zero };
    private int currentMoveDirection;

    [Header("Boundary Settings")]
    public Vector2 minBoundary; // Minimum x and y boundaries
    public Vector2 maxBoundary; // Maximum x and y boundaries

    void Start()
    {
        // Cache the transform and Rigidbody2D for quicker access
        thisTransform = this.transform;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // Set a random time delay for taking a decision (changing direction, or standing in place for a while)
        decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

        // Choose a movement direction, or stay in place
        ChooseMoveDirection();
    }

    void Update()
    {
        // Move the object in the chosen direction at the set speed
        rb.velocity = moveDirections[currentMoveDirection] * moveSpeed;

        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetFloat("xInput", rb.velocity.x);
            animator.SetFloat("yInput", rb.velocity.y);
            animator.SetBool("isMoving", rb.velocity.sqrMagnitude > 0);
        }


        if (decisionTimeCount > 0)
        {
            decisionTimeCount -= Time.deltaTime;
        }
        else
        {
            // Choose a random time delay for taking a decision (changing direction, or standing in place for a while)
            decisionTimeCount = Random.Range(decisionTime.x, decisionTime.y);

            // Choose a movement direction, or stay in place
            ChooseMoveDirection();
        }

        // Ensure the object stays within the defined boundaries
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(thisTransform.position.x, minBoundary.x, maxBoundary.x),
            Mathf.Clamp(thisTransform.position.y, minBoundary.y, maxBoundary.y)
        );

        thisTransform.position = clampedPosition;
    }

    void ChooseMoveDirection()
    {
        // Choose a random direction from the moveDirections array
        currentMoveDirection = Random.Range(0, moveDirections.Length);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Change direction when hitting an obstacle
        ChooseMoveDirection();
    }
}
