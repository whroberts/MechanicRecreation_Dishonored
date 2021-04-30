using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] FarReach _farReach = null;
    [SerializeField] MouseLook _mouseLook = null;

    PlayerMovementCC _movement;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovementCC>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _farReach.ExtendLandingPosition();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            //StartCoroutine(PlayerPhysics());
            StartCoroutine(_farReach.ReachAbility());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _farReach.ReturnLandingPosition();
        }
    }

    IEnumerator PlayerPhysics()
    {
        _movement.enabled = false;
        yield return new WaitForSeconds(2f);
        //_movement.enabled = true;
    }
}
