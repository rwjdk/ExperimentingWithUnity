using System;
using Model;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject _timerGui;
    [SerializeField] private Slider _progressbar;
    private const float TimeToGuess = 30f;
    private const float TimeToShowCorrectAnswer = 5f;
    private Quiz _quiz;
    private QuizState _state;
    private Image _timeGuiImage;
    private float _timerValue;
    public bool IsComplete { get; set; }
    
    private void Awake()
    {
        _quiz = FindObjectOfType<Quiz>();
    }
    
    private void Start()
    {
        _state = QuizState.Guessing;
        _timerValue = TimeToGuess;
        _timeGuiImage = _timerGui.GetComponent<Image>();
    }

    private void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        _state = QuizState.ShowingAnswer;
        _timerValue = TimeToShowCorrectAnswer;
    }

    private void UpdateTimer()
    {
        _timerValue -= Time.deltaTime;
        UpdateTimerGui();

        if (_timerValue <= 0)
        {
            SwitchGameState();
        }
    }

    private void SwitchGameState()
    {
        switch (_state)
        {
            case QuizState.Guessing:
                _timerValue = TimeToShowCorrectAnswer;
                _state = QuizState.ShowingAnswer;
                _quiz.OnAnswerSelected(-1); //Make a wrong guess on behalf of the player
                break;
            case QuizState.ShowingAnswer:
                _timerValue = TimeToGuess;
                _state = QuizState.Guessing;
                _quiz.GetNextQuestion();
                IsComplete = Math.Abs(_progressbar.value - _progressbar.maxValue) < 0.001;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateTimerGui()
    {
        switch (_state)
        {
            case QuizState.Guessing:
                _timeGuiImage.fillAmount = 1 / TimeToGuess * _timerValue;
                break;
            case QuizState.ShowingAnswer:
                _timeGuiImage.fillAmount = 1 / TimeToShowCorrectAnswer * _timerValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}