using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public List<Enemy> DetectedEnemies => _detectedEnemies;

    [SerializeField] private List<Enemy> _detectedEnemies;
 
    private void Start()
    {
        _detectedEnemies = new List<Enemy>();
    }

    public void UpdateDetectedEnemies(List<int> deadEnemies)
    {
        foreach (int index in deadEnemies)
        {
            _detectedEnemies.RemoveAt(index);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _detectedEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _detectedEnemies.Remove(enemy);
        }
    }
}
