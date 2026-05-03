using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthBar;

    [SerializeField] int _healAmount = 20;
    [SerializeField] int _hurtAmount = 20;
    private int _maxHealth;
    private int _health;

    private void Awake()
    {
        Assert.IsNotNull(_healthBar, "Health bar not set!");
    }

    public void Initialize( int maxHealth)
    {
        _maxHealth = maxHealth;
        _health = _maxHealth;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _health;
    }

    public void Heal()
    {
        _health += _healAmount;
        if (_health > _maxHealth) { _health= _maxHealth; }
        _healthBar.value = _health;
        Debug.Log($"Healing {_healAmount} with result of {_healthBar.value}");
    }

    public int Hurt(int damage)
    {
        _health -= damage;
        _healthBar.value = _health;
        Debug.Log($"Hurting {_hurtAmount} with result of {_healthBar.value}");
        return _health;
    }
}