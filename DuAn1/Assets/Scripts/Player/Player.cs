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
    [SerializeField] public int health;
    [SerializeField] public int food;
    [SerializeField] public int water;
    [SerializeField] public int damage;

    [SerializeField] public List<Item> inventory;


    // Children
    [SerializeField] private GameObject meleeArea;

    [SerializeField] GameManager gameManager;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] PlayerInput playerInput;

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
    [SerializeField] float attackCooldown = 0.5f;

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
        playerManager = GetComponent<PlayerManager>();
        playerInput = FindObjectOfType<PlayerInput>();
        knowFeedBack = GetComponent<KnockFeedBack>();
    }

    private void Update()
    {

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
        if (isAction() ||!canAttack) return;

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


    public void Death()
    {
        isDead = true;
        playerInput.enabled = false;
        // Show dead
    }


    public void IncreaseDamage(int amount)
    {
        damage += amount;
    }


    public bool isDeadState()
    {
        return isDead;
    }



    public PlayerData ToPlayerData()
    {
        PlayerData data = new PlayerData();
        data.playerName = userName;
        data.health = health;
        data.food = food;
        data.water = water;
        data.damage = damage;
        data.inventory = new List<string>();
        foreach (Item item in inventory)
        {
            data.inventory.Add(item.itemName);
        }
        return data;
    }

    public void LoadFromPlayerData(PlayerData data)
    {
        userName = data.playerName;
        health = data.health;
        food = data.food;
        water = data.water;
        damage = data.damage;
        inventory = new List<Item>();
/*        foreach (string itemName in data.inventory)
        {
            Item item = new Item { itemName = itemName }; // Khởi tạo Item từ tên
            inventory.Add(item);
        }*/
    }

}
