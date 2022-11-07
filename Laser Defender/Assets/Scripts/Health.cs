using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health = 50;
    [SerializeField] private int _scoreValue = 100;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _hitEffectSmall;
    [SerializeField] private bool _applyCameraShake;
    [SerializeField] private bool _isPlayer;

    private CameraShake _cameraShake;
    private AudioPlayer _audioPlayer;
    private ScoreKeeper _scoreKeeper;
    private LevelManager _levelManager;

    public int CurrentHealth => _health;

    private void Awake()
    {
        Debug.Assert(Camera.main != null, "Camera.main != null");
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            TakeDamage(damageDealer);
            PlayHitEffect(_hitEffectSmall);
            _audioPlayer.PlayDamageClip();
            ShakeCamera();
            damageDealer.HitSomething();
        }
    }

    private void ShakeCamera()
    {
        if (_applyCameraShake)
        {
            _cameraShake.Play();
        }
    }

    private void TakeDamage(DamageDealer damageDealer)
    {
        _health -= damageDealer.GetDamage();
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayHitEffect(_hitEffect);
        _audioPlayer.PlayDestroyClip();
        if (!_isPlayer)
        {
            _scoreKeeper.IncrementScore(_scoreValue);
        }
        else
        {
            _levelManager.LoadGameOver();
        }
        Destroy(gameObject);
    }

    private void PlayHitEffect(ParticleSystem effect)
    {
        ParticleSystem instance = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
