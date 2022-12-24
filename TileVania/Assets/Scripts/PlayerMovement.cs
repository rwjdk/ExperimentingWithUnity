using System.Collections;
using JetBrains.Annotations;
using Logic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    [SerializeField] private float _jumpHeight = 8;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletOrigin;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private TextMeshProUGUI _youWon;
    private Animator _animator;
    private CapsuleCollider2D _capsuleCollider;
    private int _curentSceneIndex;
    private BoxCollider2D _feetCollider;
    private bool _gameOver;
    private bool _isAlive = true;
    private Vector2 _moveInput;
    private float _normalGravity;
    private Rigidbody2D _rigidBody;

    private void Start()
    {
        _curentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _feetCollider = GetComponent<BoxCollider2D>();
        _normalGravity = _rigidBody.gravityScale;
    }

    private void Update()
    {
        if (!_isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        ClimbLadder();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isAlive && _rigidBody.IsTouchingLayers(LayerMask.GetMask(Constants.Layers.Enemy, Constants.Layers.Hazard, Constants.Layers.Water)))
        {
            Die();
        }
        else if (_isAlive && other.CompareTag(Constants.Tags.Finish))
        {
            if (GameObject.FindGameObjectsWithTag(Constants.Tags.Coin).Length == 0)
            {
                if (_gameOver)
                {
                    return;
                }

                StartCoroutine(NextLevel());
            }
        }
        else if (_isAlive && other.CompareTag(Constants.Tags.Coin))
        {
            PickUpCoin(other);
        }
    }

    private void ClimbLadder()
    {
        var layerMaskId = LayerMask.GetMask(Constants.Layers.Ladder);
        if (_capsuleCollider.IsTouchingLayers(layerMaskId))
        {
            var playerVelocity = new Vector2(_rigidBody.velocity.x, _moveInput.y * _speed);
            _rigidBody.velocity = playerVelocity;
            _rigidBody.gravityScale = 0;
            _animator.SetBool(Constants.AnimatorParameters.IsClimbing, PlayerHasVerticalSpeed());
        }
        else
        {
            _rigidBody.gravityScale = _normalGravity;
            _animator.SetBool(Constants.AnimatorParameters.IsClimbing, false);
        }
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
        if (!_isAlive)
        {
            return;
        }

        _moveInput = value.Get<Vector2>();
    }

    [UsedImplicitly]
    private void OnJump(InputValue value)
    {
        if (!_isAlive)
        {
            return;
        }

        var layerMaskId = LayerMask.GetMask(Constants.Layers.Ground);
        if (_feetCollider.IsTouchingLayers(layerMaskId))
        {
            _rigidBody.velocity += new Vector2(0f, _jumpHeight);
        }
    }

    [UsedImplicitly]
    private void OnFire(InputValue value)
    {
        if (!_isAlive)
        {
            return;
        }

        Instantiate(_bullet, _bulletOrigin.position, _bulletOrigin.rotation);
    }

    private void Run()
    {
        var playerVelocity = new Vector2(_moveInput.x * _speed, _rigidBody.velocity.y);
        _rigidBody.velocity = playerVelocity;
        _animator.SetBool(Constants.AnimatorParameters.IsRunning, PlayerHasHorizontalSpeed());
    }

    private void PickUpCoin(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        AudioSource.PlayClipAtPoint(_audioClip, Camera.current.velocity);
        Destroy(otherGameObject);
    }

    private void Die()
    {
        _isAlive = false;
        _animator.SetTrigger(Constants.AnimatorTrigger.Dying);
        _rigidBody.velocity += new Vector2(10f, 10f);
        StartCoroutine(ReloadLevel(3));
    }

    private IEnumerator ReloadLevel(int delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        OverallGameState.ProcessPlayerDeath();
    }

    private IEnumerator NextLevel()
    {
        yield return new WaitForSecondsRealtime(1);
        _curentSceneIndex++;

        if (_curentSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            _gameOver = true;
            _youWon.gameObject.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(_curentSceneIndex);
        }
    }
}