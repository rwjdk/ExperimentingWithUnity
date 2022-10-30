using JetBrains.Annotations;
using UnityEngine;

[UsedImplicitly]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    [SerializeField] private float _torque;
    private SurfaceEffector2D _surfaceEffector;
    [SerializeField] ParticleSystem _moveEffect;
    private bool _canMove = true;

    [UsedImplicitly]
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _surfaceEffector = FindObjectOfType<SurfaceEffector2D>();
    }

    [UsedImplicitly]
    void Update()
    {
        if (_canMove)
        {
            RotatePlayer();
            RespondToBoost();
        }
    }

    [UsedImplicitly]
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _moveEffect.Play();
            Debug.Log("Touching ground");
        }
    }

    [UsedImplicitly]
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _moveEffect.Stop();
            Debug.Log("in the air");
        }
    }

    public void DisableControls()
    {
        _canMove = false;
    }

    private void RespondToBoost()
    {
        _surfaceEffector.speed = 15;
        if (Input.GetKey(KeyCode.Space))
        {
            _surfaceEffector.speed = 30;
        }
    }

    private void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _rigidBody.AddTorque(_torque);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _rigidBody.AddTorque(-_torque);
        }
    }
}