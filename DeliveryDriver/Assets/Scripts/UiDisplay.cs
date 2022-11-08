using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class UiDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private TextMeshProUGUI _youWonText;
    private Stopwatch _stopWatch;

    private void Start()
    {
        _stopWatch = Stopwatch.StartNew();
        _youWonText.enabled = false;
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        _timerText.text = $"{GetElapsedTimeInSeconds()} Seconds";
    }

    public void StopTimerAndShowWonText()
    {
        _stopWatch.Stop();
        _youWonText.text = $"Good Job, You won the game in {GetElapsedTimeInSeconds()} Seconds!";
        _youWonText.enabled = true;
    }

    private int GetElapsedTimeInSeconds()
    {
        return Convert.ToInt32(_stopWatch.Elapsed.TotalSeconds);
    }
}