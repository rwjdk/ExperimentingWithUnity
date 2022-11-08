using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _finalScoreText;
    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Update()
    {
        ShowFinalScore();
    }

    public void ShowFinalScore()
    {
        _finalScoreText.text = $"Congratulations!\nYou Got as score of {_scoreKeeper.GetScore()}%";
    }
}