using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> _waves;
    [SerializeField] private float _timeBetweenWaves = 1f;
    private WaveConfig _currentWave;

    void Start()
    {
        _currentWave = _waves[0];
        StartCoroutine(SpawnEnemies());
    }

    public WaveConfig CurrentWave => _currentWave;

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            foreach (var waveConfig in _waves)
            {
                _currentWave = waveConfig;
                for (int i = 0; i < CurrentWave.EnemyPrefabs.Count; i++)
                {
                    CreateEnemy(i);
                    yield return new WaitForSeconds(_currentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
        }
    }

    private void CreateEnemy(int i)
    {
        Instantiate(
            _currentWave.EnemyPrefabs[i],
            _currentWave.StartingWaypoints.position,
            Quaternion.identity,
            transform);
    }
}
