using System;
using UnityEngine;

public class TurretUpgrade : MonoBehaviour
{
    [SerializeField] private int _upgradeInitialCost;
    [SerializeField] private int _upgradeCostIncremental;
    [SerializeField] private float _damageIncremental;
    [SerializeField] private float _delayReduce;

    public int UpgradeCost { get; set; }
    
    private TurretProjectile _turretProjectile;

    private void Start()
    {
        _turretProjectile = GetComponent<TurretProjectile>();
        UpgradeCost = _upgradeInitialCost;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            UpgradeTurret();
        }
    }

    private void UpgradeTurret()
    {
        if (CurrencyManager.Instance.TotalCoins > UpgradeCost)
        {
            _turretProjectile.Damage += _damageIncremental;
            _turretProjectile.DelayPerShot -= _delayReduce;
            CurrencyManager.Instance.RemoveCoins(UpgradeCost);
            UpgradeCost += _upgradeCostIncremental;
        }
    }
}