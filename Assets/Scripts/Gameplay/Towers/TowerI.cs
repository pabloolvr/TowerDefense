using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerI : Tower
{
    [SerializeField] private float _attacksPerSecond;
    [SerializeField] private float _attackDamage;

    protected override void ApplyEffect(Enemy enemy)
    {
        enemy.GetComponent<DamageableUnit>().Damage(_attackDamage);
    }

    protected void Update()
    {
        EffectApplication(1 / Mathf.Max(Mathf.Epsilon, _attacksPerSecond), false);
    }
}
