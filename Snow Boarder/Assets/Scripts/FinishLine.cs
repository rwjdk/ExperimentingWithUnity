using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    [UsedImplicitly]
    public class FinishLine : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _finishEffect;

        [UsedImplicitly]
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                Debug.Log("You Finished");
                _finishEffect.Play();
                GetComponent<AudioSource>().Play();
                Invoke(nameof(LoadScene), 0.5f);

            }
        }

        void LoadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
