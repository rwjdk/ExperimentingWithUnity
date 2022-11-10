using System;
using Extras;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private SpawnMode _spawnMode;

        [SerializeField] private GameObject _testGameObject;
        [SerializeField] private int _maxEnemyCount;

        [Header("Fixed Spawn Settings")] 
        [SerializeField] private float _fixedDelayBetweenSpawns;

        [Header("Random Spawn Settings")] 
        [SerializeField] private float _minRandomDelay;

        [SerializeField] private float _maxRandomDelay;
        
        private int _currentNumberOfEnemies;
        private float _spawnTimer;
        private ObjectPooler _pooler;

        private void Start()
        {
            _pooler = GetComponent<ObjectPooler>();
        }

        private void Update()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer < 0)
            {
                _spawnTimer = GetSpawnDelay();
                if (_currentNumberOfEnemies < _maxEnemyCount)
                {
                    Spawn();
                }
            }
        }

        private void Spawn()
        {
            var instance = _pooler.GetPoolObject();
            instance.SetActive(true);
            _currentNumberOfEnemies++;
        }

        private float GetSpawnDelay()
        {
            return _spawnMode switch
            {
                SpawnMode.Fixed => _fixedDelayBetweenSpawns,
                SpawnMode.Random => GetRandomDelay(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private float GetRandomDelay()
        {
            return Random.Range(_minRandomDelay, _maxRandomDelay);
        }
    }
}