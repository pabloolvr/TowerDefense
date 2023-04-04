using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public abstract class Enemy : MonoBehaviour
{
    public int GoldValue => _goldValue;
    public int ScoreValue => _scoreValue;
    public Stat BaseSpeed => _baseSpeed;
    public Transform Goal 
    { 
        get
        {
            return _goal;
        }
        set
        {
            _goal = value;
        }
    }

    protected void OnEnable()
    {
        if (Goal)
        {
            _agent.destination = Goal.position;
        }
    }

    public event Action OnGoalReach = () => { };

    [Header("Stats")]
    [SerializeField] protected DamageableUnit _damageableComponent;
    [SerializeField] protected float _healthPointsGrowth;
    [SerializeField] protected int _goldValue;
    [SerializeField] protected int _scoreValue;
    [SerializeField] protected Stat _baseSpeed;

    protected NavMeshAgent _agent;
    [SerializeField] protected Transform _goal;
    protected Transform _transform;

    protected void Awake()
    {
        _transform = transform;
        _agent = GetComponent<NavMeshAgent>();
        _baseSpeed.OnAddModifier += SetEnemySpeed;
        _baseSpeed.OnRemoveModifier += SetEnemySpeed;
        SetEnemySpeed();
    }

    protected void Update()
    {
        _transform.LookAt(Goal.position);
    }

    protected void SetEnemySpeed()
    {
        _agent.speed = _baseSpeed.Value;
    }

    public void CallOnGoalReach()
    {
        OnGoalReach();
    }

    public void IncreaseStats(int level)
    {
        _damageableComponent.MaxHealthPoints.RemoveAllModifiers();
        _damageableComponent.MaxHealthPoints.AddModifier(new StatModifier(StatModifierType.AbsolutValue, level * _healthPointsGrowth));
    }
}
