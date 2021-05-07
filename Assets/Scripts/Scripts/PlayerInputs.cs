using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] FarReach _farReach = null;
    [SerializeField] MouseLook _mouseLook = null;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _farReach.CastLandingPosition();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (_farReach._objectSelected)
            {
                _farReach.PullObject();
            } 
            else
            {
                _farReach.ReachAbilityAnimation();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _mouseLook.enabled = false;
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _mouseLook.enabled = true;
        }
    }
}
