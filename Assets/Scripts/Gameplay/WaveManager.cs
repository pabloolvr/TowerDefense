using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct EnemyWaveData
{
    public GameObject EnemyPrefab => _enemyPrefab;
    public int MaxQuantity => _maxQuantity;

    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _maxQuantity;
}

public class WaveManager : MonoBehaviour
{
    public List<Enemy> EnemyPool => _enemyPool;
    public int CurWave => _curWave;

    [SerializeField] private Transform _enemiesContainer;

    [Header("Wave Settings")]
    [SerializeField] private float _maxWavePeriod;
    [SerializeField] private int _waveGrowth;

    [Header("Enemy Positioning Settings")]
    [SerializeField] private float _maxLocalXPos;
    [SerializeField] private float _minLocalXPos;
    [SerializeField] private float _maxLocalZPos;
    [SerializeField, Range(1f, 2f)] private float _xPosOffsetMultiplier = 1.2f;
    private float _avgLocalXPos;
    private float _maxLocalPosOffset;

    [Header("Enemy Data")]
    [SerializeField] private EnemyWaveData[] _enemiesData;

    private int _maxWaveSize;
    private List<Enemy> _enemyPool;
    private float _lastWaveTime;
    private int _curWave;

    private void Awake()
    {
        _lastWaveTime = float.MinValue;
        _avgLocalXPos = (_maxLocalXPos + _minLocalXPos) / 2;
        _maxLocalPosOffset = (_maxLocalXPos - _avgLocalXPos) / _xPosOffsetMultiplier;
        _curWave = 0;
        _maxWaveSize = 0;
        SortEnemies();
    }

    private void SortEnemies()
    {
        _enemyPool = new List<Enemy>();

        Dictionary<GameObject, int> map = new Dictionary<GameObject, int>();

        for (int i = 0; i < _enemiesData.Length; i++)
        {
            _maxWaveSize += _enemiesData[i].MaxQuantity;
            map.Add(_enemiesData[i].EnemyPrefab, _enemiesData[i].MaxQuantity * 3);
        }

        int sortedEnemies = 0;
        while (sortedEnemies < _maxWaveSize) 
        {
            int sortedAux = Random.Range(0, _enemiesData.Length);
            if (map[_enemiesData[sortedAux].EnemyPrefab] > 0)
            {
                map[_enemiesData[sortedAux].EnemyPrefab]--;
                GameObject prefab = _enemiesData[sortedAux].EnemyPrefab;
                Enemy enemy = Instantiate(prefab, _enemiesContainer).GetComponent<Enemy>();
                _enemyPool.Add(enemy);
                sortedEnemies++;
            }
        }

    }

    private void Update()
    {
        if (Time.fixedTime - _lastWaveTime < _maxWavePeriod) return;
       
        SpawnWave();
    }

    private void SpawnWave()
    {
        _lastWaveTime = Time.fixedTime;       

        int waveSize = Math.Min(_maxWaveSize, (_curWave + 1) * _waveGrowth);
        int spawnedEnemies = 0;
        float curLocalPosOffset = 0;
  
        foreach (Enemy enemy in _enemyPool)
        {
            if (!enemy.gameObject.activeSelf)
            {
                enemy.IncreaseStats(_curWave);
                bool spawnedEvenEnemies = spawnedEnemies % 2 == 0;
                enemy.transform.localPosition = SetNewEnemyLocalPosition(enemy.transform.localPosition, curLocalPosOffset, spawnedEvenEnemies);
                enemy.gameObject.SetActive(true);
                spawnedEnemies++;

                // add 1 to localPosOffset whenever spawned enemies count is even
                if (spawnedEvenEnemies)
                {
                    curLocalPosOffset++;
                }

                if (spawnedEnemies == waveSize)
                {
                    break;
                }
            }
        }

        _curWave++;
    }

    private Vector3 SetNewEnemyLocalPosition(Vector3 curPos, float localPosOffset, bool positiveOffset)
    {
        float multiplier = positiveOffset? _xPosOffsetMultiplier : -_xPosOffsetMultiplier;
        float xPos = _avgLocalXPos + ((localPosOffset % _maxLocalPosOffset) * multiplier);
        float zPos = _maxLocalZPos - (2 * Mathf.Floor(localPosOffset / _maxLocalPosOffset));
        return new Vector3(xPos, curPos.y, zPos);
    }
}
