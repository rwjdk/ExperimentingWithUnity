using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private float _bulletSpeed = 4f;
    private PlayerMovement _player;
    private float _xSpeed;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();
        _xSpeed = _player.transform.localScale.x * _bulletSpeed;
    }

    void Update()
    {
        _rigidBody.velocity = new Vector2(_xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.Enemy))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }
}
