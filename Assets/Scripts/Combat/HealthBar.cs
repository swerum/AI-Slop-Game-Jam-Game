using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider _healthBar;
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

    public void Heal(int healAmount)
    {
        _health += healAmount;
        if (_health > _maxHealth) { _health= _maxHealth; }
        _healthBar.value = _health;
    }

    public int Hurt(int damage)
    {
        _health -= damage;
        _healthBar.value = _health;
        return _health;
    }
}