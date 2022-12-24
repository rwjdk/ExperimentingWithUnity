using System;
using Enemies;
using Shared;
using Turrets;
using UnityEngine;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public static Action<Enemy, float> OnEnemyHit;

        [SerializeField] protected float _moveSpeed;
    

        public float Damage { get; set; }

        protected Enemy EnemyTarget;
        private readonly float _minDistanceToDealDamage = 0.1f;
        public TurretProjectile Owner { get; set; }

        protected virtual void Update()
        {
            MoveProjectile();
            Rotate();
        }

        public void SetEnemy(Enemy enemy)
        {
            EnemyTarget = enemy;
        }

        private void Rotate()
        {
            if (EnemyTarget == null)
            {
                return;
            }

            var selfTransform = transform;
            var targetPosition = EnemyTarget.transform.position - selfTransform.position;
            float angle = Vector3.SignedAngle(selfTransform.up, targetPosition, selfTransform.forward);
            transform.Rotate(0f, 0f, angle);

        }

        protected virtual void MoveProjectile()
        {
            if (EnemyTarget == null)
            {
                return;
            }

            var enemyPosition = EnemyTarget.transform.position;
            var selfTransformPosition = transform.position;
        
            transform.position = Vector2.MoveTowards(selfTransformPosition, enemyPosition, _moveSpeed * Time.deltaTime);
            var distanceToTarget = (enemyPosition - selfTransformPosition).magnitude;
            if (distanceToTarget < _minDistanceToDealDamage)
            {
                HitTarget();
            }
        }

        protected void HitTarget()
        {
            OnEnemyHit?.Invoke(EnemyTarget, Damage);
            EnemyTarget.EnemyHealth.DealDamage(Damage);
            Owner.ResetTurretProjectile();
            ObjectPooler.ReturnToPool(gameObject);
        }

        public void ResetProjectile()
        {
            EnemyTarget = null;
            transform.localRotation = Quaternion.identity;
        }
    }
}