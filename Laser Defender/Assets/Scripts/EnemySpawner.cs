using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> _waves;
    [SerializeField] private float _timeBetweenWaves = 1f;

    public WaveConfig CurrentWave { get; private set; }

    private void Start()
    {
        CurrentWave = _waves[0];
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            foreach (var waveConfig in _waves)
            {
                CurrentWave = waveConfig;
                for (var i = 0; i < CurrentWave.EnemyPrefabs.Count; i++)
                {
                    CreateEnemy(i);
                    yield return new WaitForSeconds(CurrentWave.GetRandomSpawnTime());
                }

                yield return new WaitForSeconds(_timeBetweenWaves);
            }
        }
    }

    private void CreateEnemy(int i)
    {
        Instantiate(
            CurrentWave.EnemyPrefabs[i],
            CurrentWave.StartingWaypoints.position,
            Quaternion.Euler(0, 0, 180),
            transform);
    }
}