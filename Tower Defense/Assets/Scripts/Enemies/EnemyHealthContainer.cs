using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyHealthContainer : MonoBehaviour
    {
        [SerializeField] private Image _fillAmountImage;

        public Image FillAmountImage => _fillAmountImage;
    }
}