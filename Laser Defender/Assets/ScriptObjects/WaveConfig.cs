using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptObjects
{
    [CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private List<GameObject> _enemyPrefabs;
        [SerializeField] Transform _pathPrefab;
        [SerializeField] float _moveSpeed = 5f;
        [SerializeField] float _timeBetweenEnemySpawns = 1f;
        [SerializeField] float _spawnTimeVariance = 0f;
        [SerializeField] float _minimumSpawnTime = 0.2f;

        public float MoveSpeed => _moveSpeed;
        public Transform StartingWaypoints => _pathPrefab.GetChild(0);
        public List<Transform> Waypoints => _pathPrefab.Cast<Transform>().ToList();
        public List<GameObject> EnemyPrefabs => _enemyPrefabs;

        public float GetRandomSpawnTime()
        {
            var spawnTime = Random.Range(_timeBetweenEnemySpawns - _spawnTimeVariance, _timeBetweenEnemySpawns + _spawnTimeVariance);

            return Mathf.Clamp(spawnTime, _minimumSpawnTime, float.MaxValue);
        }
    }
}
