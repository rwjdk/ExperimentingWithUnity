using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _shootingClip;
    [SerializeField] [Range(0f, 1f)] private float _shootingVolume;
    [SerializeField] private AudioClip _damageClip;
    [SerializeField] [Range(0f, 1f)] private float _damageVolume;
    [SerializeField] private AudioClip _destroyClip;
    [SerializeField] [Range(0f, 1f)] private float _destroyVolume;

    public void PlayShootingClip()
    {
        AudioSource.PlayClipAtPoint(_shootingClip, Camera.main!.transform.position, _shootingVolume);
    }

    public void PlayDamageClip()
    {
        AudioSource.PlayClipAtPoint(_damageClip, Camera.main!.transform.position, _damageVolume);
    }

    public void PlayDestroyClip()
    {
        AudioSource.PlayClipAtPoint(_destroyClip, Camera.main!.transform.position, _destroyVolume);
    }
}