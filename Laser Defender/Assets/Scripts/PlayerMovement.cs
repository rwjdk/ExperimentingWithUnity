using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 _rawInput;
    private readonly float _moveSpeed = 8f;
    private Vector2 _minBounds;
    private Vector2 _maxBounds;
    [SerializeField] private float paddingLeft;
    [SerializeField] private float paddingRight;
    [SerializeField] private float paddingTop;
    [SerializeField] private float paddingBottom;

    private void Start()
    {
        InitBounds();
    }

    void Update()
    {
        DoMovement();
    }

    private void InitBounds()
    {
        var camera = Camera.main;
        var scale = transform.localScale;
        _minBounds = camera.ViewportToWorldPoint(new Vector2(0, 0))+scale/2f;
        _maxBounds = camera.ViewportToWorldPoint(new Vector2(1, 1))-scale/2f;
        
    }

    private void DoMovement()
    {
        

        var delta = _rawInput * (_moveSpeed * Time.deltaTime);
        var newPosition = new Vector2();
        var currentPosition = transform.position;
        newPosition.x = Mathf.Clamp(currentPosition.x + delta.x, _minBounds.x + paddingLeft, _maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(currentPosition.y + delta.y, _minBounds.y + paddingBottom, _maxBounds.y - paddingTop);
        transform.position = newPosition;
    }

    [UsedImplicitly]
    void OnMove(InputValue input)
    {
        _rawInput = input.Get<Vector2>();
    }
}
