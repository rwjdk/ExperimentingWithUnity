using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _projectileLifeTime = 5f;
    [SerializeField] private float _fireRate = 02f;
    [SerializeField] private bool _isAi;
    [SerializeField] private float _fireTimeVariance;
    [SerializeField] private float _minimumFireTime = 0.1f;

    [HideInInspector] public bool isFireing;

    private AudioPlayer _audioPlayer;

    private Coroutine _fireringCoroutine;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        if (_isAi)
        {
            isFireing = true;
        }
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFireing && _fireringCoroutine == null)
        {
            _fireringCoroutine = StartCoroutine(FireContiniously());
        }
        else if (!isFireing && _fireringCoroutine != null)
        {
            StopCoroutine(_fireringCoroutine);
            _fireringCoroutine = null;
        }
    }

    private IEnumerator FireContiniously()
    {
        while (true)
        {
            var instantiate = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
            var instanceRigidBody = instantiate.GetComponent<Rigidbody2D>();
            instanceRigidBody.velocity = transform.up * _projectileSpeed;

            Destroy(instantiate, _projectileLifeTime);

            _audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(GetRandomFireRate());
        }
    }

    public float GetRandomFireRate()
    {
        var timeToNexProjectile = Random.Range(_fireRate - _fireTimeVariance, _fireRate + _fireTimeVariance);

        return Mathf.Clamp(timeToNexProjectile, _minimumFireTime, float.MaxValue);
    }
}