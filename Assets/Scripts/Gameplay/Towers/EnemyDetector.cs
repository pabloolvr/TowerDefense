using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public List<Enemy> DetectedEnemies => _detectedEnemies;

    [SerializeField] private List<Enemy> _detectedEnemies;

    public event Action OnEnemyEnter = () => { };
    public event Action OnEnemyExit = () => { };

    private void Start()
    {
        _detectedEnemies = new List<Enemy>();
    }

    public void UpdateDetectedEnemies(List<int> deadEnemies)
    {
        foreach (int index in deadEnemies)
        {
            if (index < _detectedEnemies.Count)
            {
                _detectedEnemies.RemoveAt(index);
            }
        }

        OnEnemyExit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _detectedEnemies.Add(enemy);
        }

        OnEnemyEnter();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _detectedEnemies.Remove(enemy);
        }

        OnEnemyExit();
    }
}
