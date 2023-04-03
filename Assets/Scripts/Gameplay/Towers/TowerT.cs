using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower T deals low damage to all enemies in area.
/// </summary>
public class TowerT : Tower
{
    [SerializeField] private float _damagePerSecond;

    protected override void ApplyEffect(Enemy enemy)
    {
        enemy.GetComponent<DamageableUnit>().Damage(_damagePerSecond * Time.deltaTime);
    }

    protected void Update()
    {
        EffectApplicator(0, true);
    }
}
