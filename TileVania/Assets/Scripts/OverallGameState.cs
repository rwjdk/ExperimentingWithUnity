using UnityEngine.SceneManagement;

public static partial class OverallGameState
{
    private const int MaxLives = 999;
    public static int PlayerLives { get; private set; } = MaxLives;

    public static void ProcessPlayerDeath()
    {
        if (PlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private static  void ResetGameSession()
    {
        PlayerLives = MaxLives;
        SceneManager.LoadScene(0);
    }
 
    private static  void TakeLife()
    {
        PlayerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}