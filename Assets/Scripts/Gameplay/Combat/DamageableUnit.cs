using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableUnit : MonoBehaviour
{
    public bool IsDead { get; private set; }
    public float CurHealthPoints => _curHealthPoints;
    public float MaxHealthPoints => _maxHealthPoints.Value;

    [SerializeField] private Stat _maxHealthPoints;
    
    private float _curHealthPoints;

    public event Action OnDie = () => {};

    private void Start()
    {
        _curHealthPoints = _maxHealthPoints.Value;
    }

    private void OnEnable()
    {
        IsDead = false;
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
