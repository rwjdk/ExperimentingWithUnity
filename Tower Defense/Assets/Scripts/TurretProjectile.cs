using System;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected Transform _projectileSpawnPosition;
    
    [SerializeField] protected float _delayBetweenAttacks;
    protected float _nextAttackTime;
    protected ObjectPooler _pooler;
    private Projectile _currentProjectileLoaded;
    protected Turret _turret;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _turret = GetComponent<Turret>();
    }

    protected virtual void Update()
    {
        if (_currentProjectileLoaded == null)
        {
            LoadProjectile();
        }

        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _turret.CurrentEnemyTarget.Health.CurrentHealth > 0)
            {
                _currentProjectileLoaded.transform.parent = null;
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }

            _nextAttackTime = Time.time + _delayBetweenAttacks;
        }
    }

    protected virtual void LoadProjectile()
    {
        var projectile = _pooler.GetInstanceFromPool();
        projectile.transform.localPosition = _projectileSpawnPosition.position;
        projectile.transform.SetParent(_projectileSpawnPosition);
        _currentProjectileLoaded = projectile.GetComponent<Projectile>();
        _currentProjectileLoaded.Owner = this;
        _currentProjectileLoaded.ResetProjectile();
        projectile.SetActive(true);
    }

    public void ResetTurretProjectile()
    {
        _currentProjectileLoaded = null;
    }
}
