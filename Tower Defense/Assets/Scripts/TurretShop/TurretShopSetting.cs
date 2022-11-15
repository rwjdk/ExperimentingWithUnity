using UnityEngine;

[CreateAssetMenu(menuName = "Turret Shop Setting", fileName = "New Turret Shop Setting")]
public class TurretShopSetting : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _shopCost;
    [SerializeField] private Sprite _shopSprite;
}