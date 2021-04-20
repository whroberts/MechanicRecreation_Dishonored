using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Sword _swordScript = null;
    [SerializeField] FarReach _farReach = null;

    [SerializeField] Animator _animator = null;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

        if (Input.GetButton("Fire2"))
        {
            _farReach.ExtendLandingPosition();
        }
    }

    void Attack()
    {
        _animator.SetTrigger("Attack");
        _swordScript.SwordAttack();
    }
}
