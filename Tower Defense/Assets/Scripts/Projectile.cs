using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static event Action<Enemy, float> OnEnemyHit;

    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _damage;



    public float Damage => _damage;

    protected Enemy EnemyTarget;
    private float _minDistanceToDealDamage = 0.1f;
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
        OnEnemyHit?.Invoke(EnemyTarget, _damage);
        EnemyTarget.Health.DealDamage(_damage);
        Owner.ResetTurretProjectile();
        ObjectPooler.ReturnToPool(gameObject);
    }

    public void ResetProjectile()
    {
        EnemyTarget = null;
        transform.localRotation = Quaternion.identity;
    }
}