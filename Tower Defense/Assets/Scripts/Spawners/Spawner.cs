using System;
using System.Collections;
using Enemies;
using Shared;
using UnityEngine;
using WayPointSystem;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class Spawner : MonoBehaviour
    {
        public static Action OnWaveCompleted;

        [SerializeField] private SpawnMode _spawnMode;
        [SerializeField] private int _maxEnemyCount;
        [SerializeField] private float _fixedDelayBetweenSpawns;
        [SerializeField] private float _minRandomDelay;
        [SerializeField] private float _maxRandomDelay;
        [SerializeField] private float _delayBetweenWaves;

        private int _currentNumberOfEnemies;
        private float _spawnTimer;
        private ObjectPooler _pooler;
        private WayPoint _wayPoint;
        private int _enemiesRemaining;

        private void Start()
        {
            _pooler = GetComponent<ObjectPooler>();
            _wayPoint = GetComponent<WayPoint>();
            _enemiesRemaining = _maxEnemyCount;
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
            var instance = _pooler.GetInstanceFromPool();
            var enemy = instance.GetComponent<Enemy>();
            enemy.WayPoint = _wayPoint;
            enemy.transform.localPosition = transform.position;
            enemy.Reset();
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

        private void OnEnable()
        {
            Enemy.ReachedEnd += RecordEnemyEndLife;
            EnemyHealth.Died += RecordEnemyEndLife;
        }

        private void OnDisable()
        {
            Enemy.ReachedEnd -= RecordEnemyEndLife;
            EnemyHealth.Died -= RecordEnemyEndLife;
        }

        private void RecordEnemyEndLife(Enemy enemy)
        {
            _enemiesRemaining--;
            if (_enemiesRemaining <= 0)
            {
                OnWaveCompleted?.Invoke();
                StartCoroutine(NextWave());
            }
        }

        private IEnumerator NextWave()
        {
            yield return new WaitForSeconds(_delayBetweenWaves);
            _enemiesRemaining = _maxEnemyCount;
            _spawnTimer = 0f;
            _currentNumberOfEnemies = 0;
        }
    }
}