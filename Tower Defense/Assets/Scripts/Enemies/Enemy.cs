using System;
using Shared;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        public static event Action<Enemy> ReachedEnd;

        private Vector3 NextWayPoint => WayPoint.GetWayPointPosition(_currentWayPointIndex);
        private bool ReachedLastWayPoint => _currentWayPointIndex >= WayPoint.Points?.Length;
        private int _currentWayPointIndex;
        private EnemyHealth _enemyHealth;
        private float _originalMovementSpeed;
        private Vector3 _lastPointPosition;
        private SpriteRenderer _spriteRenderer;

        public WayPointSystem.WayPoint WayPoint { get; set; }
        public EnemyHealth EnemyHealth => _enemyHealth;

        private void Awake()
        {
            _originalMovementSpeed = _moveSpeed;
        }

        private void Start()
        {
            _currentWayPointIndex = 0;
            _enemyHealth = GetComponent<EnemyHealth>();
            _lastPointPosition = transform.position;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Move();
            Rotate();
        }

        public void StopMovement()
        {
            _moveSpeed = 0;
        }

        public void ResumeMovement()
        {
            _moveSpeed = _originalMovementSpeed;
        }


        private void Move()
        {
            if (ReachedLastWayPoint)
            {
                EndPointReached();
                return; //No more movement needed
            }

            transform.position = Vector3.MoveTowards(transform.position, NextWayPoint, _moveSpeed * Time.deltaTime);

            if (NextWayPointReached())
            {
                _lastPointPosition = transform.position;
                _currentWayPointIndex++;
            }
        }

        private void Rotate()
        {
            _spriteRenderer.flipX = NextWayPoint.x < _lastPointPosition.x;
        }

        public void EndPointReached()
        {
            ReachedEnd?.Invoke(this);
            _enemyHealth.ResetHealth();
            ObjectPooler.ReturnToPool(gameObject);
        }

        private bool NextWayPointReached()
        {
            var distanceToNextWayPoint = (transform.position - NextWayPoint).magnitude;
            return (distanceToNextWayPoint < 0.01);
        }

        public void Reset()
        {
            _moveSpeed = _originalMovementSpeed;
            _currentWayPointIndex = 0;
        }
    }
}