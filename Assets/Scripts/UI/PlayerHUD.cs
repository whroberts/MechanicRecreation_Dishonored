using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] PlayerStats _playerStats = null;

    [Header("UI Components")]
    [SerializeField] Slider _healthBar = null;
    [SerializeField] Slider _manaBar = null;

    private void Start()
    {
        _healthBar.value = _playerStats._totalPlayerHealth;
        _manaBar.value = _playerStats._totalPlayerMana;
    }

    private void Update()
    {
        CheckStats();
    }

    void CheckStats()
    {
        _healthBar.value = _playerStats._currentPlayerHealth;
        _manaBar.value = _playerStats._currentPlayerMana;
    }
}
