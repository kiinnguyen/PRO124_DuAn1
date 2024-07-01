using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private Animator _swordAnimator;
    private Rigidbody2D _rigidbody;

    // values
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        _animator.SetBool("isMoving", _rigidbody.velocity != Vector2.zero);
    }


    public bool SetAnimation(float x, float y)
    {
        try
        {
            _animator.SetFloat("xInput", x);
            _animator.SetFloat("yInput", y);

            

        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }

        return true;
    }

    public bool Attack(float x, float y)
    {
        try
        {
            _animator.SetTrigger("Attack");

            _swordAnimator.SetFloat("xInput", x);
            _swordAnimator.SetFloat("yInput", y);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }

        return true;
    }

    public void SetActionState()
    {
        //isActing = false;
    }
}
