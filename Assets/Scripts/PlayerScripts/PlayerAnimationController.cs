using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public bool _isStanding = true;
    public bool _isWalking = false;
    public bool _isSprinting = false;

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Standing", _isStanding);
    }

    private void Update()
    {
        _animator.SetBool("Standing", _isStanding);
        _animator.SetBool("Walk", _isWalking);
        _animator.SetBool("Sprint", _isSprinting);
    }

    public void IsSprinting(bool state)
    {
        _isSprinting = state;
    }

    public void IsWalking(bool state)
    {
        _isWalking = state;
    }

    public void IsStanding(bool state)
    {
        _isStanding = state;
    }
}
