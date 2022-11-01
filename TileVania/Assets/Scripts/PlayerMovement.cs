using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _moveInput;
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    [SerializeField] private float _speed = 4;
    [SerializeField] private float _jumpHeight = 8;
    [SerializeField] private float _climbSpeed = 4;
    private CapsuleCollider2D _collider;
    private float _normalGravity;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider2D>();
        _normalGravity = _rigidBody.gravityScale;
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void ClimbLadder()
    {
        var layerMaskId = LayerMask.GetMask(Constants.Layers.Ladder);
        if (_collider.IsTouchingLayers(layerMaskId))
        {
            Vector2 playerVelocity = new Vector2(_rigidBody.velocity.x, _moveInput.y * _speed);
            _rigidBody.velocity = playerVelocity;
            _rigidBody.gravityScale = 0;
        }
        else
        {
            _rigidBody.gravityScale = _normalGravity;
        }
        _animator.SetBool(Constants.AnimatorParameters.IsClimbing, PlayerHasVerticalSpeed());
    }

    private void FlipSprite()
    {
        if (PlayerHasHorizontalSpeed())
        {
            transform.localScale = new Vector2(Mathf.Sign(_moveInput.x), 1f);
        }
    }

    private bool PlayerHasHorizontalSpeed()
    {
        return Mathf.Abs(_moveInput.x) > Mathf.Epsilon;
    }
    
    private bool PlayerHasVerticalSpeed()
    {
        return Mathf.Abs(_moveInput.y) > Mathf.Epsilon;
    }

    [UsedImplicitly]
    private void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    [UsedImplicitly]
    private void OnJump(InputValue value)
    {
        var layerMaskId = LayerMask.GetMask(Constants.Layers.Ground);
        if (_collider.IsTouchingLayers(layerMaskId))
        {
            _rigidBody.velocity += new Vector2(0f, _jumpHeight);
        }
    }

    private void Run()
    {
        Vector2 playerVelocity = new Vector2(_moveInput.x * _speed, _rigidBody.velocity.y);
        _rigidBody.velocity = playerVelocity;
        _animator.SetBool(Constants.AnimatorParameters.IsRunning, PlayerHasHorizontalSpeed());

    }

}
