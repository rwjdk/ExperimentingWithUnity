using System;
using System.Globalization;
using Enemies;
using JetBrains.Annotations;
using Nodes;
using Shared;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        [SerializeField] private GameObject _turretShopPanel;
        [SerializeField] private GameObject _nodeUpgradePanel;
        [SerializeField] private GameObject _achivementPanel;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _upgradeText;
        [SerializeField] private TextMeshProUGUI _sellText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _totalLivesText;
        [SerializeField] private TextMeshProUGUI _totalCoinsText;
        [SerializeField] private TextMeshProUGUI _totalCurrentWaveText;
        [SerializeField] private TextMeshProUGUI _totalKillsText;

        private Node _currentNodeSelected;
        private int _numberOfEnemiesKilled;

        private void Start()
        {
            _numberOfEnemiesKilled = 0;
            Time.timeScale = 1f; //Needed after restart
        }

        private void Update()
        {
            UpdateWorldStats();
        }

        private void OnEnable()
        {
            Node.OnNodeSelected += OnNodeSelected;
            EnemyHealth.Died += EnemyHealth_Died;
        }

        private void EnemyHealth_Died(Enemy obj)
        {
            _numberOfEnemiesKilled++;
        }

        private void OnDisable()
        {
            Node.OnNodeSelected -= OnNodeSelected;
            EnemyHealth.Died -= EnemyHealth_Died;
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
            _nodeUpgradePanel.SetActive(true);
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


        public void CloseShopPanel()
        {
            _currentNodeSelected.HideAttackRange();
            _turretShopPanel.SetActive(false);
        }

        [UsedImplicitly]
        public void ShowAchivementPanel()
        {
            _achivementPanel.SetActive(true);
            AchievementManager.Instance.UpdateAllProgress();
        }
        
        
        public void ShowGameOverPanel()
        {
            Time.timeScale = 0;
            _totalKillsText.text = $"You killed {_numberOfEnemiesKilled} enemies";
            _gameOverPanel.SetActive(true);
        }

        [UsedImplicitly]
        public void CloseAchivementPanel()
        {
            _achivementPanel.SetActive(false);
        }

        public void CloseNodePanel()
        {
            if (_currentNodeSelected != null)
            {
                _currentNodeSelected.HideAttackRange();
            }
            _nodeUpgradePanel.SetActive(false);
        }

        [UsedImplicitly]
        public void SlowTime()
        {
            Time.timeScale = 0.5f;
        }

        [UsedImplicitly]
        public void ResumeTime()
        {
            Time.timeScale = 1f;
        }
        
        [UsedImplicitly]
        public void PauseTime()
        {
            Time.timeScale = 0f;
        }

        [UsedImplicitly]
        public void FastTime()
        {
            Time.timeScale = 2f;
        }

        [UsedImplicitly]
        public void PlayAgain()
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}