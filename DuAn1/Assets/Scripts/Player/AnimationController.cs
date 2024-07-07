using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
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

    public bool Attack()
    {
        try
        {
            _animator.SetTrigger("Attack");
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }

        return true;
    }

}
