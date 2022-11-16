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

        private Node _currentNodeSelected;

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

        public void ShowNodePanel()
        {
            _nodeUiPanel.SetActive(true);
        }


        public void CloseCloseShopPanel()
        {
            _turretShopPanel.SetActive(false);
        }
        
        public void CloseNodePanel()
        {
            _nodeUiPanel.SetActive(false);
        }

    }
}