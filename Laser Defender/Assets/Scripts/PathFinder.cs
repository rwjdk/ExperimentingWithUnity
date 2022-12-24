using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private WaveConfig _waveConfig;
    private int _wayPointIndex;
    private List<Transform> _wayPoints;

    private void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        _waveConfig = _enemySpawner.CurrentWave;
        _wayPoints = _waveConfig.Waypoints;
        transform.position = _wayPoints[_wayPointIndex].position;
    }

    private void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (_wayPointIndex < _wayPoints.Count)
        {
            var newPosition = _wayPoints[_wayPointIndex].position;
            var delta = _waveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, newPosition, delta);
            if (transform.position == newPosition)
            {
                _wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}