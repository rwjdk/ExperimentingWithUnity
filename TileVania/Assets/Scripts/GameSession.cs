using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _statusText;
    [SerializeField] private TextMeshProUGUI _coinText;

    void Update()
    {
        SetStatus();
    }

    private void SetStatus()
    {
        _statusText.text = $"{OverallGameState.PlayerLives} Lives";
        var coinsLeft = GameObject.FindGameObjectsWithTag(Constants.Tags.Coin)?.Length ?? 0;
        _coinText.text = coinsLeft == 0 ? "All Coins found. Now find the Exit!" : $"Find {coinsLeft} more coins before you can exit";
    }
}