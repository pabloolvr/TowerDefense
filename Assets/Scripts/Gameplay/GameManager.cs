using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager UIManager => _UIManager;
    public TowerPlacer TowerPlacer => _towerPlacer;
    public PlayerManager PlayerManager => _playerManager;
    public WaveManager WaveManager => _waveManager;

    [Header("References")]
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private TowerPlacer _towerPlacer;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private WaveManager _waveManager;

    private void Start()
    {
        StartUI();

        foreach (Enemy enemy in _waveManager.EnemyPool)
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

                if (_playerManager.PlayerHealthPoints == 0)
                {
                    EndGame();
                }
            };

            enemy.Goal = _playerManager.Base;
            enemy.gameObject.SetActive(false);
        }
    }

    private void StartUI()
    {
        _UIManager.UpdateHealthAmount(_playerManager.PlayerHealthPoints);
        _UIManager.UpdateGoldAmount(_playerManager.PlayerGold);
        _UIManager.UpdateScore(_playerManager.PlayerScore);
    }

    public void EndGame()
    {
        _UIManager.SetRegionsActive(false);
        _UIManager.GameOverPanel.gameObject.SetActive(true);
    }
}
