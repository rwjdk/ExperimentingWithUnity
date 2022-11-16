using System.Collections.Generic;
using System.Linq;
using Enemies;
using Shared;
using UnityEngine;

namespace Turrets
{
    [RequireComponent(typeof(TurretUpgrade))]
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float _attackRange;

        public float AttackRange => _attackRange;

        public TurretUpgrade TurretUpgrade { get; set; }

        private bool _gameStarted;
        private List<Enemy> _enemies;

        public Enemy CurrentEnemyTarget { get; set; }

        private void Start()
        {
            _gameStarted = true;
            _enemies = new List<Enemy>();
            TurretUpgrade = GetComponent<TurretUpgrade>();
        }

        private void Update()
        {
            SetCurrentEnemy();
            RotateTowardTarget();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Tags.Enemy))
            {
                var newEnemy = other.GetComponent<Enemy>();
                _enemies.Add(newEnemy);

            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(Constants.Tags.Enemy))
            {
                var leavingEnemy = other.GetComponent<Enemy>();
                lock (_enemies)
                {
                    if (_enemies.Contains(leavingEnemy))
                    {
                        _enemies.Remove(leavingEnemy);
                    }
                }
            }
        }

        private void RotateTowardTarget()
        {
            if (CurrentEnemyTarget == null)
            {
                return;
            }

            var selfTransform = transform;
            var targetPosition = CurrentEnemyTarget.transform.position - selfTransform.position;
            float angle = Vector3.SignedAngle(selfTransform.up, targetPosition, selfTransform.forward);
            transform.Rotate(0f,0f, angle);
        }

        private void SetCurrentEnemy()
        {
            if (!_enemies.Any())
            {
                CurrentEnemyTarget = null;
                return;
            }

            CurrentEnemyTarget = _enemies.First();
        }

        private void OnDrawGizmos()
        {
            if (!_gameStarted)
            {
                GetComponent<CircleCollider2D>().radius = _attackRange;
            }

            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}
