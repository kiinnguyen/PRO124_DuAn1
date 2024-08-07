using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] public string userName;
    [SerializeField] public int gold;
    [SerializeField] public int health = 100;
    [SerializeField] public int food = 100;
    [SerializeField] public int damage = 40;
    [SerializeField] public Vector2 currentPOS;
    // Children
    [SerializeField] private GameObject meleeArea;
    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] UIGameScene uiGameScene;
    // Components
    Animator _animator;
    Rigidbody2D _rigidbody;
    AudioSource _speaker;


    // Classes
    private AnimationController animation_Controller;
    private KnockFeedBack knowFeedBack;

    // values
    private float speedMove = 3f;

    private float xInput;
    private float yInput;
    private Vector2 vector2Input;

    [SerializeField] private float zRotate;

    [Header("Attack Setting")]
    [SerializeField] float attackDuration = 0.5f;

    [Header("Audio Setting")]
    [SerializeField] AudioClip attack_Sfx;

    // bool
    private bool isDashing = false;
    private bool isAttacking = false;
    private bool canAttack = true;
    private bool isDead = false;

    // Effects
    private float originalSpeed;
    private bool isSlowed = false;


    // Pause Game

    private bool isPaused = false;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _speaker = GetComponent<AudioSource>();
    }
    void Start()
    {
        // Classes
        animation_Controller = GetComponent<AnimationController>();
        gameManager = FindObjectOfType<GameManager>();
        playerManager = GetComponent<PlayerManager>();
        playerInput = GetComponent<PlayerInput>();
        knowFeedBack = GetComponent<KnockFeedBack>();
        uiGameScene = FindObjectOfType<UIGameScene>();

        originalSpeed = speedMove;
    }

    private void Update()
    {
        if (isPaused)
        {
            _rigidbody.velocity = Vector2.zero;
            _animator.SetBool("isMoving", false);
            return;
        }
    }


    private void FixedUpdate()
    {
        currentPOS = transform.position;
    }

    // Input Actions
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!isPaused)
        {
            vector2Input = context.ReadValue<Vector2>().normalized;

            if (vector2Input != Vector2.zero)
            {
                RotateAttackArea(vector2Input);
            }
            _rigidbody.velocity = new Vector2(vector2Input.x, vector2Input.y) * speedMove;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!canAttack || isPaused) return;

        if (context.performed)
        {
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
        yield return new WaitForSeconds(0.1f);
        meleeArea.SetActive(false);
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        canAttack = true;
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

    // States

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
        _rigidbody.velocity = Vector2.zero;
        _animator.SetBool("isMoving", false);
        Debug.Log("Player Paused");
    }

    void HandleResume()
    {
        isPaused = false;
        Debug.Log("Player Resumed");
    }

    // Slow effect
    public void SlowEffect()
    {
        if (!isSlowed)
        {
            StartCoroutine(SlowEffectCoroutine());
        }
    }

    private IEnumerator SlowEffectCoroutine()
    {
        isSlowed = true;
        speedMove /= 2;

        yield return new WaitForSeconds(5);

        speedMove = originalSpeed;
        isSlowed = false;
    }



    // Data Manager


    public void Death()
    {
        isDead = true;
        playerInput.enabled = false;
        uiGameScene.DeathShow();
    }


    public void IncreaseDamage(int amount)
    {
        damage += amount;
    }


    public bool isDeadState()
    {
        return isDead;
    }


    public void Die()
    {
        // Hiển thị thông báo chết
        Debug.Log("Player đã chết!");

        // Xử lý logic chết như làm cho nhân vật biến mất hoặc tải lại scene
        // Ví dụ: Destroy(gameObject); hoặc SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
