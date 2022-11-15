using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TurretShop
{
    public class TurretCard : MonoBehaviour
    {
        [SerializeField] private Image _turretImage;
        [SerializeField] private TextMeshProUGUI _turretCostText;

        public void SetupTurretButton(TurretShopSetting shopSetting)
        {
            _turretImage.sprite = shopSetting.
        }
    }
}