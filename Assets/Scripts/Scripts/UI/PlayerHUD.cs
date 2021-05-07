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
    [SerializeField] Text _grabText = null;

    private void Start()
    {
        _healthBar.value = _playerStats._totalPlayerHealth;
        _manaBar.value = _playerStats._totalPlayerMana;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UpdateStats()
    {
        _healthBar.value = _playerStats._currentPlayerHealth;
        _manaBar.value = _playerStats._currentPlayerMana;
    }

    public void ShowText(bool state)
    {
        _grabText.enabled = state;
    }
}
