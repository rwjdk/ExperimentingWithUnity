using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyComponent : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BasicConcepts _basicConcepts;
    [SerializeField] private float _moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
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

        var move = (Vector3)_basicConcepts.Player.Move.ReadValue<Vector2>();
        //transform.position += _move * _moveSpeed;
        GetComponent<Rigidbody2D>().velocity = move * _moveSpeed;
    }

    private void SetColor()
    {
        _spriteRenderer.color = Color.red;
    }
}
