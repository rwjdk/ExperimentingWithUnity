using Logic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _steerSpeed;
    [SerializeField] private float _moveSpeed;
    public bool IsBoosted { get; set; }
    
    private void Update()
    {
        MoveCar();
    }

    private void MoveCar()
    {
        //This is a crude way to do input, but used in this project for simplicity sake. Use new Input System instead like over training projects
        var horizontalAxis = Input.GetAxis(Constants.Axis.HorizontalAxisName);
        var verticalAxis = Input.GetAxis(Constants.Axis.VerticalAxisName);

        var steerAmount = -horizontalAxis * _steerSpeed * Time.deltaTime;
        var moveAmount = verticalAxis * _moveSpeed * GetBoostFactor() * Time.deltaTime;

        transform.Rotate(0, 0, steerAmount);
        transform.Translate(0, moveAmount, 0);
    }

    private int GetBoostFactor()
    {
        return IsBoosted ? 2 : 1; //Boost will be 2x Faster than normal
    }
}