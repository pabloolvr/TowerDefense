using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Transform Base => _playerBase;
    public int PlayerHealthPoints => _playerHealthPoints;
    public int PlayerGold => _playerGold;
    public int PlayerScore => _playerScore;

    [SerializeField] private Transform _playerBase;
    [SerializeField] private int _playerHealthPoints;
    [SerializeField] private int _initialPlayerGold;

    private int _playerGold;
    private int _playerScore = 0;

    private void Awake()
    {
        _playerGold = _initialPlayerGold;
        _playerScore = 0;
    }

    public void AddGoldAmount(int amount)
    {
        _playerGold += amount;
    }

    public void AddHealthPoints(int amount)
    {
        _playerHealthPoints += amount;
    }

    public void AddScorePoints(int amount)
    {
        _playerScore += amount;
    }
}
