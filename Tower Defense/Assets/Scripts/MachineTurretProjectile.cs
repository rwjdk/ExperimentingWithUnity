using UnityEngine;

public class MachineTurretProjectile : TurretProjectile
{
    [SerializeField] private bool _isDualMachine;
    [SerializeField] private float _spreadRange;

    protected override void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null)
            {
                var directionTotarget = _turret.CurrentEnemyTarget.transform.position - transform.position;
                FireProjectile(directionTotarget);
            }
            _nextAttackTime = Time.time + _delayBetweenAttacks;
        }
    }

    protected override void LoadProjectile()
    {
        //Empty
    }

    private void FireProjectile(Vector3 direction)
    {
        var instance = _pooler.GetInstanceFromPool();
        instance.transform.position = _projectileSpawnPosition.position;

        var projectile = instance.GetComponent<MachineProjectile>();
        Vector3 newDirection = direction;

        if (_isDualMachine)
        {
            var randomSpread = Random.Range(-_spreadRange, _spreadRange);
            var spread = new Vector3(0f, 0f, randomSpread);
            Quaternion spreadValue = Quaternion.Euler(spread);
            newDirection = spreadValue *  direction;
        }
        projectile.Direction = newDirection;
        instance.SetActive(true);
    }
}