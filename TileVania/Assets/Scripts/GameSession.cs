using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    private const int MaxLives = 10;
    [SerializeField] private int _playerLives = MaxLives;

    private void Awake()
    {
        int numGamesSessions = FindObjectsOfType<GameSession>().Length;
        if (numGamesSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (_playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        _playerLives = MaxLives;
        SceneManager.LoadScene(0);
    }

    private void TakeLife()
    {
        _playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
