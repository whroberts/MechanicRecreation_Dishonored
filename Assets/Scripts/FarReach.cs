using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarReach : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject _player = null;

    [Header("Ability")]
    [SerializeField] GameObject _landingPad = null;
    [SerializeField] GameObject _finalPoint = null;

    [Header("Effects")]
    [SerializeField] ParticleSystem _locationSystem = null;
    [SerializeField] Light _groundLight = null;

    float _extendingRate = 0.2f;
    float _playerRate = 0.1f;
    float _landingLocationDist;
    float _playerLerpDist;

    Collider _padCollider;

    Vector3 _originalDistance;
    RaycastHit _hit;

    private void Awake()
    {
        _padCollider = _landingPad.GetComponent<Collider>();
    }

    private void Start()
    {
        _landingLocationDist = Vector3.Distance(_landingPad.transform.position, _finalPoint.transform.position);
        _originalDistance = _finalPoint.transform.localPosition;
    }

    public void ExtendLandingPosition()
    {
        float fractionOfJourney = _extendingRate / _landingLocationDist;
        _landingPad.transform.position = Vector3.Lerp(_landingPad.transform.position, _finalPoint.transform.position, fractionOfJourney);

        _locationSystem.Play();
        _groundLight.enabled = true;
        CheckForLOS();
    }

    public void ReturnLandingPosition()
    {
        _landingPad.transform.position = this.transform.position;

        _locationSystem.Stop();
        _groundLight.enabled = false;
    }

    public IEnumerator ReachAbility()
    {
        //_player.transform.localPosition = _landingPad.transform.position;

        _playerLerpDist = Vector3.Distance(_player.transform.position, _landingPad.transform.position);
        float fractionJourney = _playerRate / _playerLerpDist;
        yield return new WaitForSeconds(2f);
        _player.transform.localPosition = Vector3.Lerp(_player.transform.localPosition, _landingPad.transform.position, fractionJourney);
        ReturnLandingPosition();
    }

    void CheckForLOS()
    {
        if (Physics.Raycast(_player.transform.position, (_landingPad.transform.position - _player.transform.position), out _hit))
        {

            Vector3 test = new Vector3(_hit.point.x, _hit.point.y, _hit.point.z - 3);
            Debug.DrawLine(_player.transform.position, test);
            Debug.Log(_hit.collider);

            if (_hit.collider.name != "LandingLocation") 
            {
                _finalPoint.transform.position = test;
            } else
            {
                //_finalPoint.transform.localPosition = _originalDistance;
            }

        }
    }
}
