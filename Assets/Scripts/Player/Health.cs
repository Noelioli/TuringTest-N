using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public Action<float> OnHealthUpdated;
    public Action OnDeath;

    public bool isDead {  get; private set; }
    private float _health;

    private void Start()
    {
        _health = _maxHealth;
        OnHealthUpdated(_maxHealth);
    }

    public void DeductHealth(float value)
    {
        if (isDead) return;

        _health -= value;

        if (_health < 0 )
        {
            isDead = true;
            OnDeath();
            _health = 0;
        }

        OnHealthUpdated(_health);
    }
}
