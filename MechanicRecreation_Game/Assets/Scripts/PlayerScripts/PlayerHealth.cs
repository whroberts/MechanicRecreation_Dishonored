using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int _totalPlayerHealth = 100;
    public int _currentPlayerHealth = 0;

    [Header("Animation")]
    [SerializeField] Animator _animator = null;

    /*
    [Header("Scripts")]
    [SerializeField] GlobalLevelController _glc = null;
    [SerializeField] PlayerHUD _hud = null;
    */

    [SerializeField] AudioClip _hurtSound = null;

    AudioSource _audioSource;
    
    //Level01Controller _lc;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        //_lc = FindObjectOfType<Level01Controller>();
        _currentPlayerHealth = _totalPlayerHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Got Hit");
    }

    public void DamagePlayer(int damage)
    {
        _currentPlayerHealth -= damage;
        _audioSource.PlayOneShot(_hurtSound, 0.6f);
        //StartCoroutine(_hud.DamagePanel());

        if (_currentPlayerHealth <= 0)
        {
            Debug.Log("Died");
            //StartCoroutine(Death());
        }
    }

    /*
    IEnumerator Death()
    {
        _animator.SetTrigger("Death");
        _lc.SaveOnExit();
        yield return new WaitForSeconds(2f);
        _glc.LoadScene(2);
    }
    */
    
}
