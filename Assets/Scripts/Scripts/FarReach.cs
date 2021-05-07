using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarReach : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject _player = null;

    [Header("Ability")]
    public GameObject _landingPad = null;
    [SerializeField] GameObject _finalPoint = null;

    [Header("Effects")]
    [SerializeField] ParticleSystem _verticleParticles = null;
    [SerializeField] ParticleSystem _horizontalParticles = null;
    [SerializeField] Light _groundLight = null;

    [Header("Interaction")]
    [SerializeField] InteractionCheck _interaction = null;

    [Header("Material")]
    [SerializeField] Material _heldObjectMaterial = null;
    [SerializeField] Material _normalObjectMaterial = null;

    float _extendingRate = 0.1f;
    float _distToFinalPoint;
    float _speed = 30.0f;
    float _timeOfCast = 0f;

    bool _isCast = false;

    public bool _objectSelected = false;

    SphereCollider _landingCollider;

    Animation _anim;
    AnimationCurve _curveX;
    AnimationCurve _curveY;
    AnimationCurve _curveZ;
    AnimationClip _clip;

    Vector3 _finalPointStartLocation;
    float _originalDistance;

    DrawLine _drawLine;
    FarReachAudio _frAudio;
    PlayerMovementCC _playerMovement;
    PlayerStats _stats;

    Gradient _blackGradient;
    Gradient darkPurpleGradient;

    GameObject _objectToGrab;
    Rigidbody _objectRB;
    MeshRenderer _objectMesh;
    SphereCollider _objectCol;

    RaycastHit _hit;
    RaycastHit _hit2;
    int _rayLayerMask = 1 << 12 | 1 << 13;

    private void Awake()
    {
        _anim = _player.GetComponent<Animation>();
        _drawLine = GetComponent<DrawLine>();
        _frAudio = GetComponent<FarReachAudio>();
        _playerMovement = _player.GetComponent<PlayerMovementCC>();
        _landingCollider = _landingPad.GetComponent<SphereCollider>();
        _stats = _player.GetComponent<PlayerStats>();
    }

    private void Start()
    {
        _rayLayerMask = ~_rayLayerMask;

        _clip = new AnimationClip();
        _clip.legacy = true;

        _distToFinalPoint = Vector3.Distance(_landingPad.transform.position, _finalPoint.transform.position);
        _originalDistance = Vector3.Distance(transform.position, _finalPoint.transform.position);

        _finalPointStartLocation = _finalPoint.transform.localPosition;


    }

    private void Update()
    {
        if (_objectToGrab != null)
        {
            _objectToGrab.transform.LookAt(_player.transform);
        }

        Debug.Log(Time.timeScale);
    }

    public void CastLandingPosition()
    {
        if (!_isCast)
        {
            _frAudio.Cast();
            _isCast = true;
            _timeOfCast = Time.time;
        }

        float fractionOfJourney = _extendingRate / _distToFinalPoint;
        _landingPad.transform.position = Vector3.Lerp(_landingPad.transform.position, _finalPoint.transform.position, fractionOfJourney);

        _verticleParticles.Play();
        _horizontalParticles.Play();
        _groundLight.enabled = true;
        _drawLine.Draw(_landingPad.transform.position);
        CheckForLOS();
        SlowTimeAbility();

        if (_timeOfCast + 2f < Time.time)
        {
            _extendingRate = 1f;
        }
    }

    public IEnumerator HoldLandingPosition()
    {
        GameObject landingPadClone = Instantiate(_landingPad);
        landingPadClone.transform.localPosition = _landingPad.transform.position;

        Component[] components = new Component[2];
        components = landingPadClone.GetComponentsInChildren(typeof(ParticleSystem));

        foreach (ParticleSystem particleSystem in components)
        {
            particleSystem.Play();
        }
        _landingPad.SetActive(false);
        _drawLine._line.colorGradient = _drawLine.CreateGradient(Color.black, _blackGradient);


        yield return new WaitForSeconds(0.5f);

        Destroy(landingPadClone);
        _landingPad.SetActive(true);
        _landingPad.transform.position = this.transform.position;
        _verticleParticles.Stop();
        _horizontalParticles.Stop();
        _groundLight.enabled = false;
        _drawLine._line.enabled = false;
        _isCast = false;
        _extendingRate = 0.1f;
        _stats.UseMana(10);
    }

    ParticleSystem.Burst CreateBurst(float probability, float time, float count)
    {
        ParticleSystem.Burst burst = new ParticleSystem.Burst();
        burst.probability = probability;
        burst.time = time;
        burst.count = count;
        return burst;
    }

    void PullObjectEffects()
    {
        _landingPad.transform.position = this.transform.position;
        _verticleParticles.Stop();
        _horizontalParticles.Stop();
        _groundLight.enabled = false;
        _drawLine._line.enabled = false;
        _isCast = false;
        _extendingRate = 0.1f;
        Destroy(_objectToGrab, 3f);

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
        _frAudio.Release();
        Time.timeScale = 1.0f;

        StartCoroutine(HoldLandingPosition());
    }

    void CheckForLOS()
    {
        
        if (Physics.Raycast(transform.position, (_landingPad.transform.position - transform.position), out _hit, Vector3.Distance(transform.position,_landingPad.transform.position), _rayLayerMask, QueryTriggerInteraction.Ignore))
        {
            if (_hit.collider.name != "LandingLocation") 
            {
                Debug.DrawLine(transform.position, _hit.point, Color.red);
                float step = _speed * Time.deltaTime;
                _landingPad.transform.position = Vector3.MoveTowards(_landingPad.transform.position, transform.position,step);
                Debug.Log("Returning to player");
            } 
            else
            {
                Debug.DrawLine(transform.position, _hit.point, Color.green);
                _finalPoint.transform.localPosition = _finalPointStartLocation;
            }
        }
        
        /*
        if (Physics.Raycast(transform.position, (_finalPoint.transform.position - transform.position), out _hit, Vector3.Distance(transform.position, _finalPoint.transform.position), _rayLayerMask, QueryTriggerInteraction.Ignore))
        {
            if (_hit.collider.name != "FinalPoint")
            {
                Debug.DrawLine(transform.position, _hit.point, Color.red);
                float step = _speed * Time.deltaTime;
                _finalPoint.transform.position = Vector3.MoveTowards(_finalPoint.transform.position, transform.position, step);
                Debug.Log("Returning to player");
            }
            else if (_hit.collider.name == "FinalPoint")
            {
                Debug.DrawLine(transform.position, _hit.point, Color.green);

                Debug.Log("Ran");

                Collider[] colliders = Physics.OverlapSphere(_finalPoint.transform.position, 1f, _sphereLayerMask, QueryTriggerInteraction.Ignore);

                foreach (var collider in colliders)
                {
                    Debug.Log(collider.name);
                }

                if (colliders[0] != null)
                {
                    _finalPoint.transform.position = colliders[0].ClosestPoint(_player.transform.position);
                }

                if (colliders[0] == null)
                {
                    _finalPoint.transform.localPosition = _finalPointStartLocation;
                }
            }
        */
    }

    public void HoldOnObject()
    {
        _objectToGrab = _interaction._objectToGrab;
        _objectMesh = _objectToGrab.GetComponent<MeshRenderer>();

        _finalPoint.transform.position = _interaction._centerOfObject;
        _objectMesh.material = _heldObjectMaterial;
        _objectSelected = true;
        _landingCollider.enabled = false;
    }

    public void ExitObject()
    {
        _finalPoint.transform.localPosition = _finalPointStartLocation;
        _objectMesh.material = _normalObjectMaterial;
        _objectSelected = false;
        _landingCollider.enabled = true;
    }

    public void PullObject()
    {
        _objectRB = _objectToGrab.GetComponent<Rigidbody>();
        _objectCol = _objectToGrab.GetComponent<SphereCollider>();

        Vector3 pullForce = new Vector3(0f, 8f, 8f);

        _objectRB.useGravity = true;
        _objectCol.isTrigger = false;

        _objectRB.AddRelativeForce(pullForce, ForceMode.Impulse);
        _objectToGrab.transform.LookAt(_objectToGrab.transform);

        PullObjectEffects();

        ExitObject();
        _stats.UseMana(10);

    }

    public void SlowTimeAbility()
    {
        if (!_playerMovement._isGrounded)
        {
            Time.timeScale = 0.3f;
        }
        else if (_playerMovement._isGrounded)
        {
            Time.timeScale = 1.0f;
        }
    }
}
