using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public static event Action ReachedEnd;

    private Vector3 NextWayPoint => WayPoint.GetWayPointPosition(_currentWayPointIndex);
    private bool ReachedLastWayPoint => _currentWayPointIndex >= WayPoint.Points.Length;
    private int _currentWayPointIndex;
    public WayPointSystem.WayPoint WayPoint { get; set; }

    private void Start()
    {
        _currentWayPointIndex = 0;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (ReachedLastWayPoint)
        {
            ReachedEnd?.Invoke();
            ObjectPooler.ReturnToPool(gameObject);
            return; //No more movement needed
        }

        transform.position = Vector3.MoveTowards(transform.position, NextWayPoint, _moveSpeed * Time.deltaTime);

        if (NextWayPointReached())
        {
            _currentWayPointIndex++;
        }
    }

    private bool NextWayPointReached()
    {
        var distanceToNextWayPoint = (transform.position - NextWayPoint).magnitude;
        return (distanceToNextWayPoint < 0.01);
    }

    public void Reset()
    {
        _currentWayPointIndex = 0;
    }
}