using System;
using System.Globalization;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TurretShop
{
    public class TurretCard : MonoBehaviour
    {
        public static Action<TurretSetting> OnPlaceTurret; 

        [SerializeField] private Image _turretImage;
        [SerializeField] private TextMeshProUGUI _turretCostText;

        public TurretSetting TurretLoaded { get; set; }

        public void SetupTurretButton(TurretSetting turretSetting)
        {
            TurretLoaded = turretSetting;
            _turretImage.sprite = turretSetting.ShopSprite;
            _turretCostText.text = turretSetting.ShopCost.ToString(CultureInfo.InvariantCulture);
        }

        public void PlaceTurret()
        {
            if (CurrencyManager.Instance.TotalCoins >= TurretLoaded.ShopCost)
            {
                CurrencyManager.Instance.RemoveCoins(TurretLoaded.ShopCost);
                OnPlaceTurret?.Invoke(TurretLoaded);
            }
        }
    }
}