using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{



    [Header("References")]
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Transform _enemiesContainer;

    private Enemy[] _enemies;

    private void Awake()
    {
        _enemies = _enemiesContainer.GetComponentsInChildren<Enemy>();
    }

    private void Start()
    {
        StartUI();

        foreach (Enemy enemy in _enemies)
        {
            enemy.GetComponent<DamageableUnit>().OnDie += () => 
            { 
                _playerManager.AddGoldAmount(Mathf.Abs(enemy.GoldValue));
                _playerManager.AddScorePoints(Mathf.Abs(enemy.ScoreValue));
                _UIManager.UpdateGoldAmount(_playerManager.PlayerGold);
                _UIManager.UpdateScore(_playerManager.PlayerScore);
            };

            enemy.OnGoalReach += () => 
            {
                _playerManager.AddHealthPoints(-1);
                _UIManager.UpdateHealthAmount(_playerManager.PlayerHealthPoints);
            };

            enemy.Goal = _playerManager.Base;
        }
    }

    private void StartUI()
    {
        _UIManager.UpdateHealthAmount(_playerManager.PlayerHealthPoints);
        _UIManager.UpdateGoldAmount(_playerManager.PlayerGold);
        _UIManager.UpdateScore(_playerManager.PlayerScore);
    }
}
