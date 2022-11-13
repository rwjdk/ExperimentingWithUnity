using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] private Transform _projectileSpawnPosition;

    private ObjectPooler _pooler;
    private Projectile _currentProjectileLoaded;
    private Turret _turret;

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>();
        _turret = GetComponent<Turret>();
    }

    private void Update()
    {
        if (_currentProjectileLoaded == null)
        {
            LoadProjectile();
        }

        if (_turret.CurrentEnemyTarget != null && _currentProjectileLoaded != null && _turret.CurrentEnemyTarget.Health.CurrentHealth > 0)
        {
            _currentProjectileLoaded.transform.parent = null;
            _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
        }
    }

    private void LoadProjectile()
    {
        var projectile = _pooler.GetPoolObject();
        projectile.transform.localPosition = _projectileSpawnPosition.position;
        projectile.transform.SetParent(_projectileSpawnPosition);
        _currentProjectileLoaded = projectile.GetComponent<Projectile>();
        projectile.SetActive(true);
        
    }
}
