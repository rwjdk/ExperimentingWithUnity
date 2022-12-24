using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private static ScoreKeeper _instance;

    public int Score { get; private set; }

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
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }


    public void IncrementScore(int addedValue)
    {
        Score += addedValue;
        Score = Mathf.Clamp(Score, 0, int.MaxValue);
    }

    public void ResetScore()
    {
        Score = 0;
    }
}