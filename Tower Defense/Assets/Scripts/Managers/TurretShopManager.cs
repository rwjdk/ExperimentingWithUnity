using TurretShop;
using UnityEngine;

namespace Managers
{
    public class TurretShopManager : MonoBehaviour
    {
        [SerializeField] private GameObject _turretCardPrefab;
        [SerializeField] private Transform _turretPanelContainer;
        [SerializeField] private TurretShopSetting[] _turretShopSettings;

        private void Start()
        {
            foreach (var turretShopSetting in _turretShopSettings)
            {
                CreateTurretCard(turretShopSetting);
            }
        }

        private void CreateTurretCard(TurretShopSetting turretShopSetting)
        {
            var instance = Instantiate(_turretCardPrefab, _turretPanelContainer.position, Quaternion.identity);
            instance.transform.SetParent(_turretPanelContainer);
            instance.transform.localScale = Vector3.one; //Hack of weird behaviour. course clip 45 did not specify in more details

            var cardButton = instance.GetComponent<TurretCard>();
            cardButton.SetupTurretButton(turretShopSetting);
        }
    }
}