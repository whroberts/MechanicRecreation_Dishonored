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

    [Header("Script")]
    [SerializeField] PlayerHUD _hud = null;

    private void Awake()
    {
        _currentPlayerHealth = _totalPlayerHealth;
        _currentPlayerMana = _totalPlayerMana;
    }

    public void DamagePlayer(int damage)
    {
        _currentPlayerHealth -= damage;
        _hud.UpdateStats();

        if (_currentPlayerHealth <= 0)
        {
            Debug.Log("Died");
        }
    }

}
