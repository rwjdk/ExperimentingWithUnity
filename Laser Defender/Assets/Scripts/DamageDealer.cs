using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int _damage = 10;

    public int GetDamage()
    {
        return _damage;
    }

    public void HitSomething()
    {
        Destroy(gameObject);
    }
}