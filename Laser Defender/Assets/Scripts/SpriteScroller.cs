using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] private Vector2 _moveSpeed;
    private Material _material;
    private Vector2 _offset;

    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        _offset = _moveSpeed * Time.deltaTime;
        _material.mainTextureOffset += _offset;
    }
}