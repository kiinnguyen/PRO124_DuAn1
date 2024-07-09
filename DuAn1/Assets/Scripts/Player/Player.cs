using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Children

    [SerializeField] private GameObject meleeArea;


    // Components
    private Animator _animator;
    private Rigidbody2D _rigidbody;


    // Classes
    private AnimationController animation_Controller;
    //
    // values
    private float speedMove = 3f;

    private float xInput;
    private float yInput;
    private Vector2 vector2Input;

    [SerializeField] private float zRotate;

    private bool isActing;

    public float damage = 5f;

    void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();


        // Classes
        animation_Controller = GetComponent<AnimationController>();

        // Values

        // Children

    }

    private void FixedUpdate()
    {
    }

    // Input Actions



    public void OnMove(InputAction.CallbackContext context)
    {
        if (isActing) return;

        vector2Input = context.ReadValue<Vector2>().normalized;

        if (vector2Input != Vector2.zero)
        {
            RotateAttackArea(vector2Input);
        }
        _rigidbody.velocity = new Vector2(vector2Input.x, vector2Input.y) * speedMove;

    }

    private float lastAttackTime;
    public float attackCooldown = 0.5f;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isActing) return;

        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }

        if (context.performed)
        {
            meleeArea.SetActive(true);
            StartCoroutine(DisableMeleeAreaAfterDelay());
            animation_Controller.Attack();

            lastAttackTime = Time.time;

        }
    }

    private IEnumerator DisableMeleeAreaAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        meleeArea.SetActive(false);
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


    private bool isAction() => isActing;

    private bool isPause()
    {
        // get bool value in systems script
        return true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with " + collision.gameObject.name);
    }
}
