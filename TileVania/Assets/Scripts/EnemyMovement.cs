using Logic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float _moveSpeed = 1f;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody.velocity = new Vector2(_moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag(Constants.Tags.Coin)) //Enemies should not react to coins
        {
            return;
        }

        _moveSpeed = -_moveSpeed;
        transform.localScale = new Vector2(-Mathf.Sign(_rigidbody.velocity.x), 1f);
    }
}