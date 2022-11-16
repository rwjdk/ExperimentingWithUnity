using System;
using System.Globalization;
using JetBrains.Annotations;
using Nodes;
using Shared;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        [SerializeField] private GameObject _turretShopPanel;
        [SerializeField] private GameObject _nodeUiPanel;
        [SerializeField] private TextMeshProUGUI _upgradeText;
        [SerializeField] private TextMeshProUGUI _sellText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _totalLivesText;
        [SerializeField] private TextMeshProUGUI _totalCoinsText;
        [SerializeField] private TextMeshProUGUI _totalCurrentWaveText;

        private Node _currentNodeSelected;

        private void Update()
        {
            UpdateWorldStats();
        }

        private void OnEnable()
        {
            Node.OnNodeSelected += OnNodeSelected;
        }

        private void OnDisable()
        {
            Node.OnNodeSelected -= OnNodeSelected;
        }

        private void OnNodeSelected(Node node)
        {
            _currentNodeSelected = node;
            if (_currentNodeSelected.IsEmpty)
            {
                ShowShopPanel();
            }
            else
            {
                ShowNodePanel();
            }
        }

        private void ShowShopPanel()
        {
            _turretShopPanel.SetActive(true);
        }

        [UsedImplicitly]
        public void SellTurret()
        {
            _currentNodeSelected.SellTurret();
            _currentNodeSelected = null;
            CloseNodePanel();
        }

        [UsedImplicitly]
        public void UpgradeTurret()
        {
            _currentNodeSelected.Turret.TurretUpgrade.UpgradeTurret();
            UpdateTurretStats();
        }

        public void ShowNodePanel()
        {
            _nodeUiPanel.SetActive(true);
            UpdateTurretStats();
        }

        private void UpdateWorldStats()
        {
            _totalCoinsText.text = CurrencyManager.Instance.TotalCoins.ToString(CultureInfo.InvariantCulture);
            _totalLivesText.text = LevelManager.Instance.NumberOfLives.ToString(CultureInfo.InvariantCulture);
            _totalCurrentWaveText.text = $"Wave {LevelManager.Instance.CurrentWave.ToString(CultureInfo.InvariantCulture)}";
        }

        private void UpdateTurretStats()
        {
            _levelText.text = $"Level {_currentNodeSelected.Turret.TurretUpgrade.Level.ToString(CultureInfo.InvariantCulture)}";
            _upgradeText.text = _currentNodeSelected.Turret.TurretUpgrade.UpgradeCost.ToString(CultureInfo.InvariantCulture);
            _sellText.text = _currentNodeSelected.Turret.TurretUpgrade.GetSellValue().ToString(CultureInfo.InvariantCulture);
        }


        public void CloseCloseShopPanel()
        {
            _currentNodeSelected.HideAttackRange();
            _turretShopPanel.SetActive(false);
        }

        public void CloseNodePanel()
        {
            if (_currentNodeSelected != null)
            {
                _currentNodeSelected.HideAttackRange();
            }
            _nodeUiPanel.SetActive(false);
        }

    }
}