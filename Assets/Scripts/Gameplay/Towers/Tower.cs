using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected TowerPositioning _towerPositioning;
    [SerializeField] protected EnemyDetector _enemyDetector;
    [SerializeField] protected float _occupationRange;
    [SerializeField] protected float _attackRange;

    protected float _lastAppliedEffectTime;

    protected virtual void Start()
    {
        _lastAppliedEffectTime = 0;
    }

    protected abstract void ApplyEffect(Enemy enemy);

    protected void EffectApplication(float effectApplicationTimer, bool inArea)
    {
        if (Time.fixedTime - _lastAppliedEffectTime >= effectApplicationTimer)
        {
            List<int> deadEnemies = new List<int>();

            for (int i = 0; i < _enemyDetector.DetectedEnemies.Count; i++)
            {
                if (_enemyDetector.DetectedEnemies[i].GetComponent<DamageableUnit>().IsDead)
                {
                    deadEnemies.Add(i);
                }
                else
                {
                    _lastAppliedEffectTime = Time.fixedTime;
                    ApplyEffect(_enemyDetector.DetectedEnemies[i]);

                    if (!inArea)
                    {
                        break;
                    }
                }
            }

            _enemyDetector.UpdateDetectedEnemies(deadEnemies);
        }
    }
}
