using System;
using System.Collections;
using System.Collections.Generic;
using ScriptObjects;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    private WaveConfig _waveConfig;
    private List<Transform> _waypoints;
    private int _waypointIndex = 0;

    private void Awake()
    {
        _enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        _waveConfig = _enemySpawner.CurrentWave;
        _waypoints = _waveConfig.Waypoints;
        transform.position = _waypoints[_waypointIndex].position;
    }

    void Update()
    {
        FollowPath();
    }

    private void FollowPath()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            var newPosition = _waypoints[_waypointIndex].position;
            var delta = _waveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, newPosition, delta);
            if (transform.position == newPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
