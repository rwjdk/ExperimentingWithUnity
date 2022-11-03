using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _rigidbody.velocity = new Vector2(_moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag(Constants.Tags.Coin))
        {
            _moveSpeed = -_moveSpeed;
            transform.localScale = new Vector2(-Mathf.Sign(_rigidbody.velocity.x), 1f);
        }
    }
}
