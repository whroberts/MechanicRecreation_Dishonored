using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] Transform _rayStart = null;
    [SerializeField] LayerMask _layersToHit;

    [Header("Effects")]
    [SerializeField] LineRenderer _laserBeam = null;
    [SerializeField] ParticleSystem _hitEnemyEffect = null;
    [SerializeField] ParticleSystem _hitEnviroEffect = null;

    [Header("Audio Clips")]
    [SerializeField] AudioClip _onShoot = null;
    [SerializeField] AudioClip _onHit = null;

    RaycastHit hit;
    Camera _camera;
    ParticleSystem _currentParticleSystem;
    AudioSource _audioSource;

    float _fireRate = 0.2f;
    float _nextPrimaryFire = 0f;
    float _shotFalloff = 20f;

    float _nextSecondaryFire = 0f;
    float _secondaryFireRate = 0.4f;

    int _weaponDamage = 10;

    private void Awake()
    {
        _camera = FindObjectOfType<Camera>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time > _nextPrimaryFire)
            {
                _nextPrimaryFire = Time.time + _fireRate;
                PirmaryFire();
            }
        } 
        
        else if (Input.GetButtonDown("Fire2"))
        {
            if (Time.time > _nextSecondaryFire)
            {
                _nextSecondaryFire = Time.time + _secondaryFireRate;
                SecondaryFire();
            }
        }
    }
    void PirmaryFire()
    {
        Vector3 shotDirection = _camera.transform.forward;
        Vector3 rayOrigin = _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

        _currentParticleSystem = null;

        _laserBeam.SetPosition(0, _rayStart.position);

        if (Physics.Raycast(rayOrigin, shotDirection, out hit, _shotFalloff,_layersToHit))
        {
            _laserBeam.SetPosition(1, hit.point);
            SetParticleEffect();
            SetSoundEffect();
            
            //GiveEnemyDamage();
        }
        else
        {
            _laserBeam.SetPosition(1, rayOrigin + (shotDirection * _shotFalloff));
        }
        StartCoroutine(ShotEffects());
    }

    void SecondaryFire()
    {

    }

    IEnumerator ShotEffects()
    {
        _laserBeam.enabled = true;
        _audioSource.PlayOneShot(_onShoot,0.75f);

        if (_currentParticleSystem != null)
        {
            _currentParticleSystem.Play();
        }

        yield return new WaitForSeconds(0.07f);
        _laserBeam.enabled = false;
    }

    void SetSoundEffect()
    {
        if (!hit.transform.gameObject.CompareTag("ScoreCube")) 
        {
            _audioSource.PlayOneShot(_onHit, 0.25f);
        }
    }

    void SetParticleEffect()
    {
        if(hit.transform.gameObject.CompareTag("Enemy"))
            {
            _currentParticleSystem = _hitEnemyEffect;
        }
        else if (hit.transform.gameObject.CompareTag("Ground") || hit.transform.gameObject.CompareTag("ScoreCube"))
        {
            _currentParticleSystem = _hitEnviroEffect;
        }

        if (_currentParticleSystem != null)
        {
            _currentParticleSystem.transform.position = hit.point;
        }
    }
    
    /*
    void GiveEnemyDamage()
    {

        EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();

        if (enemyHealth != null)
        {
            enemyHealth.DamageEnemy(_weaponDamage);
        }
    }

    */
}
