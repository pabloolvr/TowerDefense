using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private DamageableUnit _damageable;
    private Canvas _canvas;
    private Transform _healthBarTransform;
    private Image _healthBar;
    private Vector3 _cameraPos;

    void Start()
    {
        _damageable = GetComponentInParent<DamageableUnit>();
        _canvas = GetComponent<Canvas>();
        _healthBarTransform = transform.GetChild(0);
        _healthBar = _healthBarTransform.GetChild(0).GetComponentInParent<Image>();
        _cameraPos = Camera.main.transform.position;
    }

    void Update()
    {
        if (_damageable.CurHealthPoints < _damageable.MaxHealthPoints.Value)
        {
            _canvas.enabled = true;
            _healthBar.fillAmount = _damageable.CurHealthPoints / _damageable.MaxHealthPoints.Value;
            _healthBarTransform.LookAt(_cameraPos, _healthBarTransform.up);
        }
        else
        {
            _canvas.enabled = false;
        }
    }
}
