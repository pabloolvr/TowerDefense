using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public abstract class Enemy : MonoBehaviour
{
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

    [Header("Stats")]
    [SerializeField] private Stat _baseSpeed;
    [SerializeField] private Stat _goldValue;

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
}
