using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

[UsedImplicitly]
public class CrashDetector : MonoBehaviour
{
    private bool _hasCrashed;

    [SerializeField] private ParticleSystem _crashEffect;

    [UsedImplicitly]
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground") && !_hasCrashed)
        {
            _hasCrashed = true;
            Debug.Log("You bumped your head!");
            _crashEffect.Play();
            FindObjectOfType<PlayerController>().DisableControls();
            GetComponent<AudioSource>().Play();
            Invoke(nameof(LoadScene), 2f);

        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}