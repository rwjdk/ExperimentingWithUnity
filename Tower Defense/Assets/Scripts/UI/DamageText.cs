using Shared;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DamageText : MonoBehaviour
    {
        public TextMeshProUGUI Text => GetComponentInChildren<TextMeshProUGUI>();

        public void ReturnTextToPool()
        {
            transform.SetParent(null);
            ObjectPooler.ReturnToPool(gameObject);
        }
    }
}