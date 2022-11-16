using Managers;
using UnityEngine;

namespace Turrets
{
    public class TurretUpgrade : MonoBehaviour
    {
        [SerializeField] private int _upgradeInitialCost;
        [SerializeField] private int _upgradeCostIncremental;
        [SerializeField] private float _damageIncremental;
        [SerializeField] private float _delayReduce;
        [SerializeField][Range(0,1)] private float _sellPercentage;

        public float SellPercentage { get; set; }
        public int UpgradeCost { get; set; }
        public int Level { get; set; }

        private TurretProjectile _turretProjectile;

        private void Start()
        {
            _turretProjectile = GetComponent<TurretProjectile>();
            UpgradeCost = _upgradeInitialCost;
            Level = 1;
            SellPercentage = _sellPercentage;
        }

        public void UpgradeTurret()
        {
            if (CurrencyManager.Instance.TotalCoins > UpgradeCost)
            {
                _turretProjectile.Damage += _damageIncremental;
                _turretProjectile.DelayPerShot -= _delayReduce;
                CurrencyManager.Instance.RemoveCoins(UpgradeCost);
                UpgradeCost += _upgradeCostIncremental;
                Level++;
            }
        }

        public int GetSellValue()
        {
            return Mathf.RoundToInt(UpgradeCost * _sellPercentage);
        }
    }
}