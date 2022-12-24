using System;
using System.Collections.Generic;
using Model;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    [SerializeField] private List<QuestionScriptObject> _questions = new();
    [SerializeField] private TextMeshProUGUI _questionTextGui;
    [SerializeField] private GameObject[] _answerButtons;
    [SerializeField] private Sprite _defaultAnswerSprite;
    [SerializeField] private Sprite _correctAnswerSprite;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private Slider _progressbar;

    private QuestionScriptObject _currentQuestion;
    private ScoreKeeper _scoreKeeper;
    private Timer _timer;

    private void Awake()
    {
        _timer = FindObjectOfType<Timer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        _progressbar.maxValue = _questions.Count;
        _progressbar.value = 0;
        GetNextQuestion();
    }

    public void GetNextQuestion()
    {
        if (_questions.Count == 0)
        {
            return;
        }

        SetButtonStates(ButtonState.Default);
        GetRandomQuestion();
        DisplayQuestion();
        _scoreKeeper.IncrementQuestionsSeen();
    }

    private void GetRandomQuestion()
    {
        var index = Random.Range(0, _questions.Count);
        _currentQuestion = _questions[index];
        lock (_questions)
        {
            if (_questions.Contains(_currentQuestion))
            {
                _questions.Remove(_currentQuestion);
            }
        }
    }

    private void DisplayQuestion()
    {
        _questionTextGui.text = _currentQuestion.Question;
        for (var i = 0; i < _answerButtons.Length; i++)
        {
            var answerButtonCaption = _answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            answerButtonCaption.text = _currentQuestion.Answers[i];
        }
    }

    public void OnAnswerSelected(int selectedIndex)
    {
        if (_currentQuestion.CorrectAnswerIndex == selectedIndex)
        {
            _scoreKeeper.IncrementCorrectAnswers();
            _questionTextGui.text = "Correct!";
            var buttonImage = _answerButtons[selectedIndex].GetComponent<Image>();
            buttonImage.sprite = _correctAnswerSprite;
        }
        else
        {
            var correctAnswer = _currentQuestion.Answers[_currentQuestion.CorrectAnswerIndex];
            if (selectedIndex == -1)
            {
                _questionTextGui.text = $"Out of time - The right answer was\n'{correctAnswer}'";
                SetButtonSprite(_currentQuestion.CorrectAnswerIndex, _correctAnswerSprite);
            }
            else
            {
                _questionTextGui.text = $"Incorrect - The right answer was\n'{correctAnswer}'";
                SetButtonSprite(_currentQuestion.CorrectAnswerIndex, _correctAnswerSprite);
            }
        }

        SetButtonStates(ButtonState.Disabled);
        _timer.CancelTimer();
        _scoreText.text = $"Score: {_scoreKeeper.GetScore()}%";
        _progressbar.value++;
    }

    private void SetButtonSprite(int index, Sprite sprite)
    {
        _answerButtons[index].GetComponent<Image>().sprite = sprite;
    }

    private void SetButtonStates(ButtonState state)
    {
        for (var i = 0; i < _answerButtons.Length; i++)
        {
            var answerButton = _answerButtons[i];
            var buttonGui = answerButton.GetComponent<Button>();
            switch (state)
            {
                case ButtonState.Default:
                    buttonGui.interactable = true;
                    SetButtonSprite(i, _defaultAnswerSprite);
                    break;
                case ButtonState.Disabled:
                    buttonGui.interactable = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}