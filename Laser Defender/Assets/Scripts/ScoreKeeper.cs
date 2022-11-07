using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int _score;

    public int Score => _score;

    private static ScoreKeeper _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }


    public void IncrementScore(int addedValue)
    {
        _score += addedValue;
        _score = Mathf.Clamp(_score, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        _score = 0;
    }
}