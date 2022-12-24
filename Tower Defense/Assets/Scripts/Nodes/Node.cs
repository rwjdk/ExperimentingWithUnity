using System;
using JetBrains.Annotations;
using Managers;
using Turrets;
using UnityEngine;

namespace Nodes
{
    public class Node : MonoBehaviour
    {
        public static Action<Node> OnNodeSelected;
        public static Action OnTurrentSold;

        [SerializeField] private GameObject _attackRangeSprite;
        private Vector3 _rangeOriginalSize;
        private float _rangeSize;
        public Turret Turret { get; set; }

        public bool IsEmpty => Turret == null;

        private void Start()
        {
            _rangeSize = _attackRangeSprite.GetComponent<SpriteRenderer>().bounds.size.y;
            _rangeOriginalSize = _attackRangeSprite.transform.localScale;
        }

        public void SetTurret(Turret turret)
        {
            Turret = turret;
        }

        [UsedImplicitly]
        public void SelectTurret()
        {
            OnNodeSelected?.Invoke(this);
            if (!IsEmpty)
            {
                ShowTurretInfo();
            }
        }

        [UsedImplicitly]
        public void SellTurret()
        {
            if (!IsEmpty)
            {
                CurrencyManager.Instance.AddCoins(Turret.TurretUpgrade.GetSellValue());
                Destroy(Turret.gameObject);
                Turret = null;
                _attackRangeSprite.SetActive(false);
                OnTurrentSold?.Invoke();
            }
        }

        private void ShowTurretInfo()
        {
            _attackRangeSprite.SetActive(true);
            _attackRangeSprite.transform.localScale = _rangeOriginalSize * Turret.AttackRange / (_rangeSize / 2);
        }

        public void HideAttackRange()
        {
            _attackRangeSprite.SetActive(false);
        }
    }
}