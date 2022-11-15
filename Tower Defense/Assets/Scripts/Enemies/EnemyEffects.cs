using System.Globalization;
using Managers;
using Projectiles;
using UI;
using UnityEngine;

namespace Enemies
{
    public class EnemyEffects : MonoBehaviour
    {
        [SerializeField] private Transform _textDamageSpawnPosition;

        private Enemy _enemy;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
        }

        private void OnEnable()
        {
            Projectile.OnEnemyHit += Projectile_OnEnemyHit;
        }

        private void OnDisable()
        {
            Projectile.OnEnemyHit -= Projectile_OnEnemyHit;
        }

        private void Projectile_OnEnemyHit(Enemy enemy, float damage)
        {
            if (enemy == _enemy)
            {
                GameObject newInstance = DamageTextManager.Instance.Pooler.GetInstanceFromPool();
                var damageText = newInstance.GetComponent<DamageText>().Text;
                damageText.text = damage.ToString(CultureInfo.InvariantCulture);

                newInstance.transform.SetParent(_textDamageSpawnPosition);
                newInstance.transform.position = _textDamageSpawnPosition.position;
                newInstance.SetActive(true);
            }
        }
    }
}