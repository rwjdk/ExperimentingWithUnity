using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private ParticleSystem _hitEffect;
    [SerializeField] private ParticleSystem _hitEffectSmall;
    [SerializeField] private bool _applyCameraShake;

    private CameraShake _cameraShake;
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
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
        health -= damageDealer.GetDamage();
        if (health <= 0)
        {
            PlayHitEffect(_hitEffect);
            _audioPlayer.PlayDestroyClip();
            Destroy(gameObject);
        }
    }

    private void PlayHitEffect(ParticleSystem effect)
    {
        ParticleSystem instance = Instantiate(effect, transform.position, Quaternion.identity);
        Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
    }
}
