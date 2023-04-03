using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tower I deals high damage to a single enemy at a time.
/// </summary>
public class TowerI : Tower
{
    [SerializeField] private float _attacksPerSecond;
    [SerializeField] private float _attackDamage;

    protected override void ApplyEffect(Enemy enemy)
    {
        enemy.GetComponent<DamageableUnit>().Damage(_attackDamage);
    }

    protected override void Update()
    {
        base.Update();
        EffectApplicator(1 / Mathf.Max(Mathf.Epsilon, _attacksPerSecond), false);
    }
}
