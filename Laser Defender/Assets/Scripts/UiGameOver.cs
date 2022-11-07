using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiGameOver : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI _finalScoreText;
    private ScoreKeeper _scoreKeeper;

    private void Start()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        _finalScoreText.text = $"You got {_scoreKeeper.Score} points.{Environment.NewLine}Nicely Done!";
    }
}
