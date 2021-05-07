using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCheck : MonoBehaviour
{
    [Header("Outside Information")]
    [SerializeField] FarReach _farReach = null;

    [Header("Object Information")]
    [Tooltip("")]
    public GameObject _objectToGrab;
    public Vector3 _sizeOfObject;
    public Vector3 _centerOfObject;

    Vector3 _empty;

    private void OnTriggerEnter(Collider other)
    {
        _objectToGrab = other.gameObject;
        _sizeOfObject = other.bounds.size;
        _centerOfObject = other.bounds.center;
    }

    private void OnTriggerStay(Collider other)
    {
        _farReach.HoldOnObject();
    }

    private void OnTriggerExit(Collider other)
    {
        _objectToGrab = null;
        _sizeOfObject = _empty;
        _centerOfObject = _empty;
        _farReach.ExitObject();
    }
}
