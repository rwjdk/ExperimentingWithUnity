using Logic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _torque;
    [SerializeField] private ParticleSystem _moveEffect;
    private bool _canMove = true;
    private Rigidbody2D _rigidBody;
    private SurfaceEffector2D _surfaceEffector;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _surfaceEffector = FindObjectOfType<SurfaceEffector2D>();
        CrashDetector.Crashing += CrashDetector_Crashing;
    }

    private void CrashDetector_Crashing()
    {
        DisableControls();
    }

    private void Update()
    {
        if (!_canMove)
        {
            return;
        }
        RotatePlayer();
        RespondToBoost();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (HasGroundTag(other))
        {
            _moveEffect.Play();
            Debug.Log("Touching ground");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (HasGroundTag(other))
        {
            _moveEffect.Stop();
            Debug.Log("in the air");
        }
    }

    private static bool HasGroundTag(Collision2D other)
    {
        return other.gameObject.CompareTag(Constants.Tags.Ground);
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