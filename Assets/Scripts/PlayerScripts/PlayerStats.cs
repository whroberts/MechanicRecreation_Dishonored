using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int _totalPlayerHealth = 100;
    public int _currentPlayerHealth = 0;

    [Header("Mana")]
    public int _totalPlayerMana = 100;
    public int _currentPlayerMana = 0;

    [Header("Animation")]
    [SerializeField] Animator _animator = null;

    [SerializeField] AudioClip _hurtSound = null;

    AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _currentPlayerHealth = _totalPlayerHealth;
        _currentPlayerMana = _totalPlayerMana;
    }

    public void DamagePlayer(int damage)
    {
        _currentPlayerHealth -= damage;
        _audioSource.PlayOneShot(_hurtSound, 0.6f);

        if (_currentPlayerHealth <= 0)
        {
            Debug.Log("Died");
        }
    }

}
