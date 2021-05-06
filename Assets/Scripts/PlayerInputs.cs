using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] FarReach _farReach = null;
    [SerializeField] MouseLook _mouseLook = null;

    PlayerMovementCC _movement;

    float _fireRate = 1f;
    float _lastShot = 0.0f;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovementCC>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _farReach.ExtendLandingPosition();
            _lastShot = Time.time;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _farReach.ReachAbilityAnimation();
            _lastShot = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _farReach.ReturnLandingPosition();
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
