using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarReachAudio : MonoBehaviour
{
    [Header("AudiClips")]
    [SerializeField] AudioClip AimClip = null;
    [SerializeField] AudioClip AimSpellClip = null;
    [SerializeField] AudioClip GrabBodyClip = null;
    [SerializeField] AudioClip CastReleaseClip1 = null;
    [SerializeField] AudioClip CastReleaseClip2 = null;

    AudioSource _audioSource;

    AudioClip[] _castReleaseClips = new AudioClip[2];

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _castReleaseClips[0] = CastReleaseClip1;
        _castReleaseClips[1] = CastReleaseClip2;
    }

    public void Cast()
    {
        _audioSource.clip = AimClip;
        _audioSource.Play();
        _audioSource.PlayOneShot(AimSpellClip);
    }

    public void Release()
    {
        _audioSource.PlayOneShot(_castReleaseClips[Random.Range(0, 1)]);
    }

    public void GrabObject()
    {

    }

    public void GrabBody()
    {

    }
}
