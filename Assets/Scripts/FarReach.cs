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
    [SerializeField] ParticleSystem _verticleParticles = null;
    [SerializeField] ParticleSystem _horizontalParticles = null;
    [SerializeField] LineRenderer _line = null;
    [SerializeField] Light _groundLight = null;

    [Header("Animation")]
    [SerializeField] AnimationClip _reachAbilityAnimation = null;

    float _extendingRate = 0.2f;
    float _playerRate = 1f;
    float _landingLocationDist;
    float _playerLerpDist;

    float _speed = 10.0f;

    Animation _anim;
    AnimationCurve _curveX;
    AnimationCurve _curveY;
    AnimationCurve _curveZ;
    AnimationClip _clip;

    RaycastHit _hit;

    private void Awake()
    {
        _anim = _player.GetComponent<Animation>();
    }

    private void Start()
    {
        _clip = new AnimationClip();
        _clip.legacy = true;

        _landingLocationDist = Vector3.Distance(_landingPad.transform.position, _finalPoint.transform.position);
    }

    public void ExtendLandingPosition()
    {
        float fractionOfJourney = _extendingRate / _landingLocationDist;
        _landingPad.transform.position = Vector3.Lerp(_landingPad.transform.position, _finalPoint.transform.position, fractionOfJourney);

        _verticleParticles.Play();
        _horizontalParticles.Play();
        _groundLight.enabled = true;
        DrawLine();
        CheckForLOS();
    }

    public void ReturnLandingPosition()
    {
        _landingPad.transform.position = this.transform.position;

        _verticleParticles.Stop();
        _horizontalParticles.Stop();
        _groundLight.enabled = false;
        _line.enabled = false;
    }

    public void ReachAbilityAnimation()
    {
        Keyframe[] keysX = new Keyframe[2];
        keysX[0] = new Keyframe(0f, _player.transform.position.x);
        keysX[1] = new Keyframe(0.5f, _landingPad.transform.position.x);
        _curveX = new AnimationCurve(keysX);

        Keyframe[] keysY = new Keyframe[2];
        keysY[0] = new Keyframe(0f, _player.transform.position.y);
        keysY[1] = new Keyframe(0.5f, _landingPad.transform.position.y);
        _curveY = new AnimationCurve(keysY);

        Keyframe[] keysZ = new Keyframe[2];
        keysZ[0] = new Keyframe(0f, _player.transform.position.z);
        keysZ[1] = new Keyframe(0.5f, _landingPad.transform.position.z);
        _curveZ = new AnimationCurve(keysZ);


        _clip.SetCurve("", typeof(Transform), "localPosition.x", _curveX);
        _clip.SetCurve("", typeof(Transform), "localPosition.y", _curveY);
        _clip.SetCurve("", typeof(Transform), "localPosition.z", _curveZ);

        _anim.AddClip(_clip, "Temp");
        _anim.Play("Temp");

        ReturnLandingPosition();
    }

    void CheckForLOS()
    {
        if (Physics.Raycast(_player.transform.position, (_landingPad.transform.position - _player.transform.position), out _hit))
        {
            Vector3 test = new Vector3(_hit.point.x, _hit.point.y, _hit.point.z);
            Debug.DrawLine(_player.transform.position, _hit.point, Color.white);
            Debug.Log(_hit.collider);

            if (_hit.collider.name != "LandingLocation") 
            {
                float step = _speed * Time.deltaTime;
                _landingPad.transform.position = Vector3.MoveTowards(_landingPad.transform.position, _player.transform.position,step);
            }
        }
    }
    
    void DrawLine()
    {
        _line.enabled = true;
        _line.SetPosition(0, this.transform.position);
        _line.SetPosition(2, _landingPad.transform.position);
        Vector3 midpoint = new Vector3((_line.GetPosition(0).x + _line.GetPosition(2).x) / 2, (_line.GetPosition(0).y + _line.GetPosition(2).y) / 2, (_line.GetPosition(0).z + _line.GetPosition(2).z) / 2);
        Vector3 topPoint = new Vector3(midpoint.x, midpoint.y+0.5f, midpoint.z);
        _line.SetPosition(1, topPoint);
    }
}
