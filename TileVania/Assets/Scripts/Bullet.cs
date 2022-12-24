using Logic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float BulletSpeed = 7f;
    private PlayerMovement _player;
    private Rigidbody2D _rigidBody;
    private float _speed;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();
        _speed = _player.transform.localScale.x * BulletSpeed;
    }

    private void Update()
    {
        _rigidBody.velocity = new Vector2(_speed, 0f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.Enemy)) //Enemy Hit!
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}