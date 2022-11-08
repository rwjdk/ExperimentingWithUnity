using UnityEngine.SceneManagement;

namespace Logic
{
    public class SceneManagerHelper
    {
        public void RestartCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}