using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    private ScoreKeeper _scoreKeeper;

    // Start is called before the first frame update
    [UsedImplicitly]
    void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Update()
    {
        ShowFinalScore();
    }

    public void ShowFinalScore()
    {
        _finalScoreText.text = $"Congratulations!\nYou Got as score of {_scoreKeeper.GetScore()}%";
    }
}
