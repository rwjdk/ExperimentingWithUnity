using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int _correctAnswers;
    private int _questionsSeen;

    public void IncrementCorrectAnswers()
    {
        _correctAnswers++;
    }

    public void IncrementQuestionsSeen()
    {
        _questionsSeen++;
    }

    public int GetScore()
    {
        return _questionsSeen == 0 ? 100 : Convert.ToInt32((float)_correctAnswers / (float)_questionsSeen * 100);
    }
}
