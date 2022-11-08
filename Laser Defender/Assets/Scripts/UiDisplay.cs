using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiDisplay : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Slider _healthSlider;
    private Health _playerHealth;
    private ScoreKeeper _scorekeeper;

    private void Start()
    {
        _playerHealth = _player.GetComponent<Health>();
        _healthSlider.maxValue = _playerHealth.CurrentHealth;
        _healthSlider.value = _playerHealth.CurrentHealth;
        _scorekeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        _healthSlider.value = _playerHealth.CurrentHealth;
        _scoreText.text = _scorekeeper.Score.ToString(CultureInfo.InvariantCulture).PadLeft(9, '0');
    }
}