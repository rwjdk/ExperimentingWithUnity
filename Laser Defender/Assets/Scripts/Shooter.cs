using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] private float fireRate = 02f;

    [Header("AI")]
    [SerializeField] private bool _isAi;
    [SerializeField] float _fireTimeVariance = 0f;
    [SerializeField] float _minimumFireTime = 0.1f;

    [HideInInspector]
    public bool isFireing;

    private Coroutine _fireringCoroutine;

    private void Start()
    {
        if (_isAi)
        {
            isFireing = true;
        }
    }

    void Update()
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
            var instantiate = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var instanceRigidBody = instantiate.GetComponent<Rigidbody2D>();
            instanceRigidBody.velocity = transform.up * projectileSpeed;

            Destroy(instantiate, projectileLifeTime);

            

            yield return new WaitForSeconds(GetRandomFireRate());
        }
    }

    public float GetRandomFireRate()
    {
        var timeToNexProjectile = Random.Range(fireRate - _fireTimeVariance, fireRate + _fireTimeVariance);

        return Mathf.Clamp(timeToNexProjectile, _minimumFireTime, float.MaxValue);
    }
}
