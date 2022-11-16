using System;
using Managers;
using Shared;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public static event Action<Enemy> Died;
        public static event Action<Enemy> Hurt;

        [SerializeField] private GameObject _healtBarPrefab;
        [SerializeField] private Transform _barPosition;
        [SerializeField] private float _initialHealth;
        [SerializeField] private float _maxHealth;
        private Enemy _enemy;
        private Image _healthBar;
        public float CurrentHealth { get; set; }

        private void Start()
        {
            CreateHealthBar();
            CurrentHealth = _initialHealth;
            _enemy = GetComponent<Enemy>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                DealDamage(5f);
            }

            _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHealth / _maxHealth, Time.deltaTime * 10f); //???
        }
        
        private void CreateHealthBar()
        {
            var newBar = Instantiate(_healtBarPrefab, _barPosition.position, Quaternion.identity);
            newBar.transform.SetParent(transform);

            var enemyHealthContainer = newBar.GetComponent<EnemyHealthContainer>();
            _healthBar = enemyHealthContainer.FillAmountImage;
        }

        public void DealDamage(float damageReceived)
        {
            CurrentHealth -= damageReceived;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
            else
            {
                Hurt?.Invoke(_enemy);
            }
        }

        private void Die()
        {
            AchievementManager.Instance.AddProgress(1, AchivementId.Kill100Enemies, AchivementId.Kill20Enemies, AchivementId.Kill50Enemies);
            Died?.Invoke(_enemy);
        }

        public void ResetHealth()
        {
            CurrentHealth = _initialHealth;
            _healthBar.fillAmount = 1;
        }
    }
}