using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Rigidbody2D _pivot;
    [SerializeField] private float _respawnTime;

    private Camera _camera;
    private Rigidbody2D _currentBallRidgidBody;
    private SpringJoint2D _currentBallSpringJoint;
    private bool _isDragging;

    private void Start()
    {
        _camera = Camera.main;
        StartCoroutine(SpawnNewBall(0));
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
        var worldPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        worldPosition = _camera.ScreenToWorldPoint(worldPosition);
        _currentBallRidgidBody.position = worldPosition;
    }

    private IEnumerator SpawnNewBall(float delay)
    {
        yield return new WaitForSeconds(delay);

        var newBall = Instantiate(_ballPrefab);
        newBall.transform.position = _pivot.position;
        _currentBallRidgidBody = newBall.GetComponent<Rigidbody2D>();
        _currentBallSpringJoint = newBall.GetComponent<SpringJoint2D>();
        _currentBallSpringJoint.connectedBody = _pivot;
    }

    private void LaunchBall()
    {
        _currentBallRidgidBody.isKinematic = false;
        _isDragging = false;
        _currentBallRidgidBody = null;

        StartCoroutine(ReleaseBallFromJoint(0.1f));
        StartCoroutine(SpawnNewBall(_respawnTime));
    }

    private IEnumerator ReleaseBallFromJoint(float delay)
    {
        yield return new WaitForSeconds(delay);
        _currentBallSpringJoint.enabled = false;
        _currentBallSpringJoint = null;
    }
}