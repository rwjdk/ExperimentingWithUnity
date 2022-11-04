using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveConfig : ScriptableObject
{
    [SerializeField] Transform _pathPrefab;
    [SerializeField] float _moveSpeed = 5f;

    public float MoveSpeed => _moveSpeed;
    public Transform StartingWaypoints => _pathPrefab.GetChild(0);
    public List<Transform> Waypoints => _pathPrefab.Cast<Transform>().ToList();
}
