using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI Text => GetComponentInChildren<TextMeshProUGUI>();

    public void ReturnTextToPool()
    {
        transform.SetParent(null);
        ObjectPooler.ReturnToPool(gameObject);
    }
}