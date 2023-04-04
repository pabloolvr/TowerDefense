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
    [SerializeField] private float _firstWaveDelay;
    [SerializeField] private float _maxWavePeriod;
    [SerializeField] private int _waveGrowth;

    [Header("Enemy Positioning Settings")]
    [SerializeField] private float _maxLocalXPos;
    [SerializeField] private float _minLocalXPos;
    [SerializeField] private float _maxLocalZPos;
    private float _avgLocalXPos;
    private float _maxLocalPosOffset;

    [Header("Enemy Data")]
    [SerializeField] private EnemyWaveData[] _enemiesData;

    private int _maxWaveSize;
    private List<Enemy> _enemyPool;
    private List<Enemy> _spawnedEnemies;
    private float _lastWaveTime;
    private int _curWave;

    private void Awake()
    {
        _lastWaveTime = float.MinValue;
        _avgLocalXPos = (_maxLocalXPos + _minLocalXPos) / 2;
        _maxLocalPosOffset = _maxLocalXPos - _avgLocalXPos;
        _curWave = 0;
        _maxWaveSize = 0;
        SortEnemies();
    }

    private void SortEnemies()
    {
        _spawnedEnemies = new List<Enemy>();
        _enemyPool = new List<Enemy>();

        Dictionary<GameObject, int> map = new Dictionary<GameObject, int>();

        for (int i = 0; i < _enemiesData.Length; i++)
        {
            _maxWaveSize += _enemiesData[i].MaxQuantity;
            map.Add(_enemiesData[i].EnemyPrefab, _enemiesData[i].MaxQuantity);
        }

        int sortedEnemies = 0;
        while (sortedEnemies < _maxWaveSize) 
        {
            int sortedAux = Random.Range(0, _enemiesData.Length);
            if (map[_enemiesData[sortedAux].EnemyPrefab] > 0)
            {
                GameObject prefab = _enemiesData[sortedAux].EnemyPrefab;
                Enemy enemy = Instantiate(prefab/*, prefab.transform.position, prefab.transform.rotation*/, _enemiesContainer).GetComponent<Enemy>();
                //enemy.transform.localPosition = prefab.transform.localPosition;
                _enemyPool.Add(enemy);
                sortedEnemies++;
            }
        }

    }

    private void Start()
    {
        //StartCoroutine(StartWaveManager());
        //SpawnWave();
    }

    private void Update()
    {
        if (Time.fixedTime - _lastWaveTime < _maxWavePeriod) return;
       
        SpawnWave();
    }

    private void SpawnWave()
    {
        _lastWaveTime = Time.fixedTime;       

        int waveSize = (_curWave + 1) * _waveGrowth;
        int spawnedEnemies = 0;
        float curLocalPosOffset = 0;
  
        for (int i = 0; i < _enemyPool.Count; i++)
        {
            GameObject enemyObject = _enemyPool[i].gameObject;

            if (!enemyObject.activeSelf)
            {
                _enemyPool[i].IncreaseStats(_curWave);
                bool isEvenIteration = i % 2 == 0;
                enemyObject.transform.localPosition = SetNewEnemyLocalPosition(enemyObject.transform.localPosition, curLocalPosOffset, isEvenIteration);
                enemyObject.SetActive(true);
                spawnedEnemies++;

                // add 1 to localPosOffset whenever i is even
                if (isEvenIteration)
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
        float multiplier = positiveOffset? 1 : -1;
        float xPos = _avgLocalXPos + ((localPosOffset % _maxLocalPosOffset) * multiplier);
        float zPos = _maxLocalZPos - Mathf.Floor(localPosOffset / _maxLocalPosOffset);
        return new Vector3(xPos, curPos.y, zPos);
    }
}
