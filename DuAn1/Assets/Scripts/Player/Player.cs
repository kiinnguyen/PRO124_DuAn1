using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Children
    [SerializeField] private GameObject meleeArea;

    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerInput playerInput;

    // Components
    Animator _animator;
    Rigidbody2D _rigidbody;
    AudioSource _speaker;


    // Classes
    private AnimationController animation_Controller;

    // values
    private float speedMove = 3f;

    private float xInput;
    private float yInput;
    private Vector2 vector2Input;

    [SerializeField] private float zRotate;

    [Header("Attack Setting")]
    [SerializeField] float attackDuration = 0.5f;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float damage = 5f;

    [Header("Dash Setting")]
    [SerializeField] float dashSpeed = 5f;
    [SerializeField] float dashDuration = 0.1f;
    [SerializeField] float dashCoolDown = 1f;


    [Header("Audio Setting")]
    [SerializeField] AudioClip attack_Sfx;

    // bool
    private bool isDashing = false;
    private bool isAttacking = false;
    private bool canAttack = true;
    private bool canDash = true;
    private bool isDead = false;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _speaker = GetComponent<AudioSource>();

        // Classes
        animation_Controller = GetComponent<AnimationController>();
        gameManager = FindObjectOfType<GameManager>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (isDead) return;
        if (Input.GetKeyDown(KeyCode.H))
        {
            _animator.SetTrigger("Hurt");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            _animator.SetTrigger("Death");
            Death();
        }
    }


    private void FixedUpdate()
    {
    }

    // Input Actions
    public void OnMove(InputAction.CallbackContext context)
    {
        if (isAction()) return;

        vector2Input = context.ReadValue<Vector2>().normalized;

        if (vector2Input != Vector2.zero)
        {
            RotateAttackArea(vector2Input);
        }
        _rigidbody.velocity = new Vector2(vector2Input.x, vector2Input.y) * speedMove;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isAction() || !canAttack) return;

        if (context.performed)
        {
            _rigidbody.velocity = Vector2.zero;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        canAttack = false;

        animation_Controller.Attack();
        _speaker.PlayOneShot(attack_Sfx);
        meleeArea.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        meleeArea.SetActive(false);
        isAttacking = false;
        canAttack = true;
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (isAction() || !canDash) return;

        if (context.performed)
        {
            _rigidbody.velocity = Vector2.zero;
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        _animator.SetTrigger("Dash");
        _rigidbody.AddForce(vector2Input * dashSpeed, ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        canDash = true;
    }

    // Actions
    private void RotateAttackArea(Vector2 vector)
    {
        animation_Controller.SetAnimation(vector.x, vector.y);

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

    // Checking states
    private bool isAction() => isDashing || isAttacking;

    private bool isPause()
    {
        // get bool value in systems script
        return true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
    }


    // Data Manager

    public void TakeDamage(int damage)
    {
        gameManager.TakeDamage(damage);
    }

    public void Death()
    {
        isDead = true;
        playerInput.enabled = false;
        // Show dead
    }

    public bool isDeadState()
    {
        return isDead;
    }
}
