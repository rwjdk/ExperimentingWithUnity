using Enemies;
using Projectiles;
using UnityEngine;

namespace Turrets
{
    public class TankTurretProjectile : TurretProjectile
    {
        protected override void Update()
        {
            if (Time.time > _nextAttackTime)
            {
                if (_turret.CurrentEnemyTarget != null && _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0)
                {
                    FireProjectile(_turret.CurrentEnemyTarget);
                }
                _nextAttackTime = Time.time + _delayBetweenAttacks;
            }
        }

        protected override void LoadProjectile()
        {
            //Empty
        }

        private void FireProjectile(Enemy enemy)
        {
            var instance = _pooler.GetInstanceFromPool();
            instance.transform.position = _projectileSpawnPosition.position;
            var projectile = instance.GetComponent<Projectile>();
            _currentProjectileLoaded = projectile;
            _currentProjectileLoaded.Owner = this;
            _currentProjectileLoaded.ResetProjectile();
            _currentProjectileLoaded.SetEnemy(enemy);
            _currentProjectileLoaded.Damage = Damage;
            instance.SetActive(true);
        }
    }
}