using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower T deals low damage to all enemies in area.
/// </summary>
public class TowerT : Tower
{
    public float DamagePerSecond => _damagePerSecond;

    [SerializeField] private float _damagePerSecond;
    [SerializeField] private float _damagePerSecondGrowth;

    public override void IncreaseLevel()
    {
        base.IncreaseLevel();
        _damagePerSecond += _damagePerSecondGrowth;
    }

    protected override void ApplyEffect(Enemy enemy)
    {
        enemy.GetComponent<DamageableUnit>().Damage(_damagePerSecond * Time.deltaTime);
    }

    protected override void Update()
    {
        base.Update();
        EffectApplicator(0, true);
    }
}
