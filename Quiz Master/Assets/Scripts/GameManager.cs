using Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private EndScreen _endScreen;
    private Quiz _quiz;
    private Timer _timer;

    private void Awake()
    {
        _quiz = FindObjectOfType<Quiz>();
        _endScreen = FindObjectOfType<EndScreen>();
        _timer = FindObjectOfType<Timer>();
    }

    private void Start()
    {
        SetQuizVisibility(Visibility.Show);
        SetEndScreenVisibility(Visibility.Hide);
    }

    private void Update()
    {
        if (_timer.IsComplete)
        {
            SetQuizVisibility(Visibility.Hide);
            SetEndScreenVisibility(Visibility.Show);
        }
    }

    private void SetEndScreenVisibility(Visibility visibility)
    {
        _endScreen.gameObject.SetActive(visibility == Visibility.Show);
    }

    private void SetQuizVisibility(Visibility visibility)
    {
        _quiz.gameObject.SetActive(visibility == Visibility.Show);
    }

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}