using UnityEngine;

namespace TurretShop
{
    [CreateAssetMenu(menuName = "Turret Shop Setting", fileName = "New Turret Shop Setting")]
    public class TurretSetting : ScriptableObject
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _shopCost;
        [SerializeField] private Sprite _shopSprite;

        public GameObject Prefab => _prefab;

        public int ShopCost => _shopCost;

        public Sprite ShopSprite => _shopSprite;
    }
}