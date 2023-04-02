using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private DamageableUnit _damageable;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _healthBar;

    void Start()
    {
        _damageable = GetComponentInParent<DamageableUnit>();
        _canvas = GetComponent<Canvas>();
        _healthBar = transform.GetChild(1).GetComponentInParent<Image>();
    }

    void Update()
    {
        if (_damageable.CurHealthPoints < _damageable.MaxHealthPoints)
        {
            _canvas.enabled = true;
            _healthBar.fillAmount= _damageable.CurHealthPoints / _damageable.MaxHealthPoints;
        }
        else
        {
            _canvas.enabled = false;
        }
    }
}
