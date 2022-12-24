using Logic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statusText;
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Update()
    {
        SetStatus();
    }

    private void SetStatus()
    {
        _statusText.text = $"{Logic.OverallGameState.PlayerLives} Lives";
        var coinsLeft = GameObject.FindGameObjectsWithTag(Constants.Tags.Coin)?.Length ?? 0; //Could be done better by at start of level load find numver of coins and then decrease value every-time a coin is picked up.
        _coinText.text = coinsLeft == 0 ? "All Coins found. Now find the Exit!" : $"Find {coinsLeft} more coins before you can exit";
    }
}