using System;
using Nodes;
using Turrets;
using TurretShop;
using UnityEngine;

namespace Managers
{
    public class TurretShopManager : MonoBehaviour
    {
        [SerializeField] private GameObject _turretCardPrefab;
        [SerializeField] private Transform _turretPanelContainer;
        [SerializeField] private TurretSetting[] _turretShopSettings;

        private Node _currentNodeSelected;

        private void Start()
        {
            foreach (var turretShopSetting in _turretShopSettings)
            {
                CreateTurretCard(turretShopSetting);
            }
        }

        private void CreateTurretCard(TurretSetting turretSetting)
        {
            var instance = Instantiate(_turretCardPrefab, _turretPanelContainer.position, Quaternion.identity);
            instance.transform.SetParent(_turretPanelContainer);
            instance.transform.localScale = Vector3.one; //Hack of weird behaviour. course clip 45 did not specify in more details
            var cardButton = instance.GetComponent<TurretCard>();
            cardButton.SetupTurretButton(turretSetting);
        }

        private void OnEnable()
        {
            Node.OnNodeSelected += OnNodeSelected;
            Node.OnTurrentSold += OnTurrentSold;
            TurretCard.OnPlaceTurret += PlaceTurret;
        }

        private void OnTurrentSold()
        {
            _currentNodeSelected = null;
        }

        private void OnDisable()
        {
            Node.OnNodeSelected -= OnNodeSelected;
            Node.OnTurrentSold -= OnTurrentSold;
            TurretCard.OnPlaceTurret -= PlaceTurret;
        }

        private void OnNodeSelected(Node node)
        {
            _currentNodeSelected = node;
        }

        private void PlaceTurret(TurretSetting turretLoaded)
        {
            if (_currentNodeSelected != null)
            {
                var parentTransform = _currentNodeSelected.transform;
                var instance = Instantiate(turretLoaded.Prefab, parentTransform.position, Quaternion.identity, parentTransform);
                var turretPlaced = instance.GetComponent<Turret>();
                _currentNodeSelected.SetTurret(turretPlaced);
                UiManager.Instance.CloseCloseShopPanel();
            }
        }
    }
}