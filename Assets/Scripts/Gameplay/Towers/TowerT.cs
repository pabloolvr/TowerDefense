using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerT : Tower
{
    [SerializeField] private float _damagePerSecond;

    protected override void ApplyEffect(Enemy enemy)
    {
        enemy.GetComponent<DamageableUnit>().Damage(_damagePerSecond * Time.deltaTime);
    }

    protected void Update()
    {
        EffectApplication(0, true);
    }
}
