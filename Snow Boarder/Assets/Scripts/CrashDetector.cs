using Logic;
using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] private ParticleSystem _crashEffect;
    private readonly SceneManagerHelper _sceneManagerHelper;
    private AudioSource _audioSource;
    private bool _hasCrashed;
    private PlayerController _playerController;

    public CrashDetector()
    {
        _sceneManagerHelper = new SceneManagerHelper();
    }

    private void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_hasCrashed && other.CompareTag(Constants.Tags.Ground))
        {
            _hasCrashed = true;
            Debug.Log("You bumped your head!");
            _crashEffect.Play();
            _playerController.DisableControls();
            _audioSource.Play();
            Invoke(nameof(RestartScene), 2f);
        }
    }

    private void RestartScene()
    {
        _sceneManagerHelper.RestartCurrentScene();
    }
}