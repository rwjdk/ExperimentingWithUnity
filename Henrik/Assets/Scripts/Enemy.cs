using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _scaleUp;

    [SerializeField] private GameObject _missile;

    [SerializeField] private GameObject _healthbarPrefab;
    [SerializeField] private Transform _healthbarPosition;
    private GameObject _h;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidBody;
    private Henrik _input;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _input = new Henrik();
        _input.Player.Move.Enable();
        _input.Player.Fire.Enable();
        _input.Player.Fire.performed += Fire_performed;
        _h = Instantiate(_healthbarPrefab);
    }

    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _spriteRenderer.color = Color.yellow;
        GameObject missileGameObject = Instantiate(_missile);
        missileGameObject.transform.position = transform.position;
    }

    private void Update()
    {
        //Debug.Log("HEJ");
        transform.localScale = new Vector3(_scaleUp, _scaleUp);
        //_scaleUp += 0.001f;
        _h.transform.position = _healthbarPosition.position;

        _rigidBody.velocity = _input.Player.Move.ReadValue<Vector2>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.CompareTag("Ground"))
        if(other.gameObject.GetComponent<Ground>() != null)
        {
            _h.transform.localScale *= 0.75f;
            other.gameObject.GetComponent<Ground>().DamageGround();
        }
    }
}
