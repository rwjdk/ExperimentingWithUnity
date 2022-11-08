using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _paddingLeft;
    [SerializeField] private float _paddingRight;
    [SerializeField] private float _paddingTop;
    [SerializeField] private float _paddingBottom;
    private readonly float _moveSpeed = 8f;
    private Vector2 _maxBounds;
    private Vector2 _minBounds;
    private Vector2 _rawInput;
    private Shooter _shooter;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        InitBounds();
    }

    private void Update()
    {
        DoMovement();
    }

    private void InitBounds()
    {
        var main = Camera.main;
        var scale = transform.localScale;
        _minBounds = main!.ViewportToWorldPoint(new Vector2(0, 0)) + scale / 2f;
        _maxBounds = main!.ViewportToWorldPoint(new Vector2(1, 1)) - scale / 2f;
    }

    private void DoMovement()
    {
        var delta = _rawInput * (_moveSpeed * Time.deltaTime);
        var newPosition = new Vector2();
        var currentPosition = transform.position;
        newPosition.x = Mathf.Clamp(currentPosition.x + delta.x, _minBounds.x + _paddingLeft, _maxBounds.x - _paddingRight);
        newPosition.y = Mathf.Clamp(currentPosition.y + delta.y, _minBounds.y + _paddingBottom, _maxBounds.y - _paddingTop);
        transform.position = newPosition;
    }

    [UsedImplicitly]
    private void OnMove(InputValue input)
    {
        _rawInput = input.Get<Vector2>();
    }

    [UsedImplicitly]
    private void OnFire(InputValue input)
    {
        _shooter.isFireing = input.isPressed;
    }
}