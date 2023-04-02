using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform _playerBase;
    [SerializeField] private Transform _enemiesContainer;
    
    private Enemy[] _enemies;

    private void Awake()
    {
        _enemies = _enemiesContainer.GetComponentsInChildren<Enemy>();
    }

    private void Start()
    {        
        foreach (Enemy enemy in _enemies)
        {
            enemy.Goal = _playerBase;
        }
    }
}
