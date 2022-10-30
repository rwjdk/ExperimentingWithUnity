using System;
using Assets.Scripts.Model;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private readonly float _timeToGuess = 30f;
    private readonly float _timeToShowCorrectAnswer = 5f;
    private float _timerValue;
    private QuizState _state;
    [SerializeField] private GameObject _timerGui;
    [SerializeField] private Slider _progressbar;

    private Image _timeGuiImage;
    private Quiz _quiz;
    public bool IsComplete;


    [UsedImplicitly]
    void Awake()
    {
        _quiz = FindObjectOfType<Quiz>();
    }


    [UsedImplicitly]
    void Start()
    {
        _state = QuizState.Guessing;
        _timerValue = _timeToGuess;
        _timeGuiImage = _timerGui.GetComponent<Image>();
    }

    [UsedImplicitly]
    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        _state = QuizState.ShowingAnswer;
        _timerValue = _timeToShowCorrectAnswer;
    }

    void UpdateTimer()
    {

        _timerValue -= Time.deltaTime;
        switch (_state)
        {
            case QuizState.Guessing:
                _timeGuiImage.fillAmount = 1 / _timeToGuess * _timerValue;
                break;
            case QuizState.ShowingAnswer:
                _timeGuiImage.fillAmount = 1 / _timeToShowCorrectAnswer * _timerValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_timerValue <= 0)
        {
            switch (_state)
            {
                case QuizState.Guessing:
                    _timerValue = _timeToShowCorrectAnswer;
                    _state = QuizState.ShowingAnswer;
                    _quiz.OnAnswerSelected(-1);
                    break;
                case QuizState.ShowingAnswer:
                    _timerValue = _timeToGuess;
                    _state = QuizState.Guessing;
                    _quiz.GetNextQuestion();
                    IsComplete = Math.Abs(_progressbar.value - _progressbar.maxValue) < 0.001;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
