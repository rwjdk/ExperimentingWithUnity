using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BallHandler : MonoBehaviour
{
    [SerializeField] private GameObject _ballPrefab;
    [SerializeField] private Rigidbody2D _pivot;
    [SerializeField] private float _respawnTime;
    [SerializeField] private TextMeshProUGUI _youWonText;
    [SerializeField] private TextMeshProUGUI _shotsText;

    private Camera _camera;
    private Rigidbody2D _currentBallRidgidBody;
    private SpringJoint2D _currentBallSpringJoint;
    private bool _isDragging;
    private bool _won;
    private int _numberOfShots;

    private void Start()
    {
        _numberOfShots = 0;
        _camera = Camera.main;
        StartCoroutine(SpawnNewBall(0));
    }

    private void Update()
    {
        UpdateShotsText();

        if (_won)
        {
            return;
        }

        CheckWinCondition();
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

    private void UpdateShotsText()
    {
        _shotsText.text = "Short" + Environment.NewLine + _numberOfShots;
    }

    private void CheckWinCondition()
    {
        var towerBlocks = GameObject.FindGameObjectsWithTag("TowerBlock");
        foreach (var towerBlock in towerBlocks)
        {
            if (IsTargetVisible(towerBlock))
            {
                return;
            }
        }

        //we end here is none of the towerblocks are visisble
        _youWonText.gameObject.SetActive(true);
    }

    private IEnumerator SpawnNewBall(float delay)
    {
        yield return new WaitForSeconds(delay);

        var newBall = Instantiate(_ballPrefab);
        newBall.transform.position = _pivot.position;
        _currentBallRidgidBody = newBall.GetComponent<Rigidbody2D>();
        _currentBallSpringJoint = newBall.GetComponent<SpringJoint2D>();
        _currentBallSpringJoint.connectedBody = _pivot;
        _numberOfShots++;
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

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private bool IsTargetVisible(GameObject go)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        var point = go.transform.position;
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }
}