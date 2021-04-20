using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarReach : MonoBehaviour
{
    [Header("Ability")]
    [SerializeField] GameObject _landingPad = null;

    [SerializeField] LineRenderer _line = null;

    Vector3[] _positions;
    float _speed = 1f;

    Transform _landingLocation;
    BoxCollider _landingCollider;
    Rigidbody _landingRigidbody;

    private void Start()
    {
        _landingLocation = _landingPad.GetComponent<Transform>();
        _landingCollider = _landingPad.GetComponent<BoxCollider>();
        _landingRigidbody = _landingPad.GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    public void ExtendLandingPosition()
    {
        _landingRigidbody.MovePosition(_landingLocation.forward);
        Debug.DrawLine(this.transform.position, _landingLocation.position);
    }

    void Reach()
    {
        
    }
}
