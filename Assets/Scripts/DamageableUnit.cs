using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableUnit : MonoBehaviour
{
    [SerializeField] private float _healthPoints;

    public event Action OnDie = () => {};

    public void Damage(float damage)
    {
        if (_healthPoints > damage)
        {
            _healthPoints -= damage;
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        OnDie();
    }
}
