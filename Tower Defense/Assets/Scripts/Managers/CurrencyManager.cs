using Enemies;
using Shared;
using UnityEngine;

namespace Managers
{
    public class CurrencyManager : Singleton<CurrencyManager>
    {
        [SerializeField] private int _defaultStartCoins ;

        private readonly string _currencySaveKey = "MY_GAME_CURRENCY";

        public int TotalCoins { get; set; }

        private void Start()
        {
            PlayerPrefs.DeleteKey(_currencySaveKey);
            LoadCoins();
        }

        private void LoadCoins()
        {
            TotalCoins = PlayerPrefs.GetInt(_currencySaveKey, _defaultStartCoins);
        }

        public void AddCoins(int amount)
        {
            TotalCoins += amount;
            Persist();
        }

        public void RemoveCoins(int amount)
        {
            if (TotalCoins >= amount)
            {
                TotalCoins -= amount;
                Persist();
            }
        }

        private void Persist()
        {
            PlayerPrefs.SetInt(_currencySaveKey, TotalCoins);
            PlayerPrefs.Save();
        }

        private void OnEnable()
        {
            EnemyHealth.Died += EnemyDied;
        }

        private void EnemyDied(Enemy obj)
        {
            TotalCoins += 1;
        }

        private void OnDisable()
        {
            EnemyHealth.Died -= EnemyDied;
        }
    }
}