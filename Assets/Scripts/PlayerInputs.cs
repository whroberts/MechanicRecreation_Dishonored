using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] Animator _animator = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _animator.SetBool("Weapons",true);
        }

        if (Input.GetButtonDown("Fire2"))
        {

        }
    }
}
