using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _currentBallRidgidBody;
    [SerializeField] private SpringJoint2D _currentBallSpringJoint;
    private Camera _camera;
    private bool _isDragging;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_currentBallRidgidBody == null)
        {
            return;
        }

        if (Touchscreen.current == null || !Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (_isDragging)
            {
                LaunchBall();
            }
            
            return;
        }
        _currentBallRidgidBody.isKinematic = true;
        _isDragging = true;
        Vector2 worldPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        worldPosition = _camera.ScreenToWorldPoint(worldPosition);
        _currentBallRidgidBody.position = worldPosition;
    }

    private void LaunchBall()
    {
        _currentBallRidgidBody.isKinematic = false;
        _isDragging = false;
        _currentBallRidgidBody = null;

        StartCoroutine(ReleaseBallFromJoint(0.5f));
    }

    private IEnumerator ReleaseBallFromJoint(float delay)
    {
        yield return new WaitForSeconds(delay);
        _currentBallSpringJoint.enabled = false;
        _currentBallSpringJoint = null;
    }
}