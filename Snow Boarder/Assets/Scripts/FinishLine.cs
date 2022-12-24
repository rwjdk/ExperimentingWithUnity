using Logic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem _finishEffect;
    private readonly SceneManagerHelper _sceneManagerHelper;
    private AudioSource _audioSource;

    public FinishLine()
    {
        _sceneManagerHelper = new SceneManagerHelper();
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(Constants.Tags.Player))
        {
            return;
        }

        Debug.Log("You Finished");
        _finishEffect.Play();
        _audioSource.Play();
        Invoke(nameof(RestartScene), 0.5f);
    }

    private void RestartScene()
    {
        _sceneManagerHelper.RestartCurrentScene();
    }
}