using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadGame()
    {
        FindObjectOfType<ScoreKeeper>()?.ResetScore();
        SceneManager.LoadScene("Game");
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver", 2f));
    }

    public IEnumerator WaitAndLoad(string sceneManager, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneManager);
    }
}
