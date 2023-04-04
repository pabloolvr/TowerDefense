using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableUnit : MonoBehaviour
{
    public bool IsDead { get; private set; }
    public float CurHealthPoints => _curHealthPoints;
    public Stat MaxHealthPoints => _maxHealthPoints;

    [SerializeField] private Stat _maxHealthPoints;
    
    private float _curHealthPoints;

    public event Action OnDie = () => {};

    private void OnEnable()
    {
        IsDead = false;
        _curHealthPoints = _maxHealthPoints.Value;
    }

    public void Damage(float damage)
    {
        if (_curHealthPoints > damage)
        {
            _curHealthPoints -= damage;
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;
        gameObject.SetActive(false);
        OnDie();
    }
}
