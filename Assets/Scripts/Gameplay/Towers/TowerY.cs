using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Tower Y slows all enemies in area.
/// </summary>
public class TowerY : Tower
{
    public float Slow => _slowPercent;

    [SerializeField] private float _slowPercent;
    [SerializeField] private float _maxSlowPercent;
    [SerializeField] private float _slowPercentGrowth;

    private StatModifier _statModifier;
    private Enemy[] _curDetectedEnemies;

    protected override void Start()
    {
        base.Start();
        _slowPercent = Mathf.Abs(_slowPercent);
        _statModifier = new StatModifier(StatModifierType.RelativeValueNonAdditive, -_slowPercent);
        _curDetectedEnemies = _enemyDetector.DetectedEnemies.ToArray();
        _enemyDetector.OnEnemyEnter += SlowEnemiesOnRange;
        _enemyDetector.OnEnemyExit += UnslowEnemiesNotOnRange;
    }

    private void SlowEnemiesOnRange()
    {
        Enemy[] newDetectedEnemies = _enemyDetector.DetectedEnemies.ToArray();

        // select enemies that are in newDetectedEnemies and not in _curDetectedEnemies
        IEnumerable<Enemy> query = from newEnemies in newDetectedEnemies.Except(_curDetectedEnemies)
                                   select newEnemies;

        foreach (Enemy enemy in query)
        {
            enemy.BaseSpeed.AddModifier(_statModifier);
        }

        _curDetectedEnemies = newDetectedEnemies;
    }

    private void UnslowEnemiesNotOnRange()
    {
        Enemy[] newDetectedEnemies = _enemyDetector.DetectedEnemies.ToArray();

        // select enemies that are in _curDetectedEnemies and not in newDetectedEnemies
        IEnumerable<Enemy> query = from newEnemies in _curDetectedEnemies.Except(newDetectedEnemies)
                                   select newEnemies;

        foreach (Enemy enemy in query)
        {
            enemy.BaseSpeed.RemoveModifier(_statModifier);
        }

        _curDetectedEnemies = newDetectedEnemies;
    }

    protected override void Update()
    {
        base.Update();
        //EffectApplicator(0, true);
    }

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();
        _slowPercent += _slowPercentGrowth;
        _slowPercent = Mathf.Min(_maxSlowPercent, _slowPercent);
        _statModifier.Value = _slowPercent;
    }

    protected override void ApplyEffect(Enemy enemy)
    {
        //enemy.BaseSpeed.AddModifier(_statModifier);
    }
}
