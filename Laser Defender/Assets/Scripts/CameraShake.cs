using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _shakeDuration = 0.3f;
    [SerializeField] private float _shakeMagnitude = 0.2f;

    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float eplasedTime = 0;
        while (eplasedTime < _shakeDuration)
        {
            transform.position = _initialPosition + (Vector3)Random.insideUnitCircle * _shakeMagnitude;
            eplasedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = _initialPosition + (Vector3)Random.insideUnitCircle * _shakeMagnitude;
    }
}