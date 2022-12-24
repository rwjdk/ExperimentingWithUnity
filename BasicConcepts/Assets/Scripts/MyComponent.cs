using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyComponent : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BasicConcepts _basicConcepts;
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        //Option 3
        _basicConcepts = new BasicConcepts();
        _basicConcepts.Player.Enable();
        _basicConcepts.Player.Fire.performed += Fire_performed;
        
    }
    
    private void Fire_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        SetColor();
    }

    //Option 2
    /*
    void OnFire()
    {
        SetColor();
    }*/

    // Update is called once per frame
    void Update()
    {
        //Option 1 (Old System)
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetColor();
        }*/

        Move();
    }

    private void Move()
    {
        var move = _basicConcepts.Player.Move.ReadValue<Vector2>();
        _rigidBody.velocity = move * _moveSpeed;
    }

    private void SetColor()
    {
        _spriteRenderer.color = Color.red;
    }
}
