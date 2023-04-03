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
            _agent.destination = _goal.position;
        }
    }

    public event Action OnGoalReach = () => { };

    [Header("Stats")]
    [SerializeField] private int _goldValue;
    [SerializeField] private int _scoreValue;
    [SerializeField] private Stat _baseSpeed;

    protected NavMeshAgent _agent;
    protected Transform _goal;
    protected Transform _transform;

    protected void Awake()
    {
        _transform = transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    protected void Update()
    {
        _transform.LookAt(Goal.position);
    }

    public void CallOnGoalReach()
    {
        OnGoalReach();
    }
}
