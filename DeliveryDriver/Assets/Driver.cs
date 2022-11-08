using JetBrains.Annotations;
using UnityEngine;

[UsedImplicitly]
public class Driver : MonoBehaviour
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    [SerializeField] float _steerSpeed = 150;
    [SerializeField] float _moveSpeed = 5;
    public bool IsBoosted;

    void Update()
    {
        var horizontalAxis = Input.GetAxis(HorizontalAxisName);
        var verticalAxis = Input.GetAxis(VerticalAxisName);

        float steerAmount = -horizontalAxis * _steerSpeed * Time.deltaTime;

        float moveAmount;
        if (IsBoosted)
        {
            moveAmount = verticalAxis * _moveSpeed * 3 * Time.deltaTime;
        }
        else
        {
            moveAmount = verticalAxis * _moveSpeed * Time.deltaTime;
        }

        //Make the car move
        transform.Rotate(0, 0, steerAmount);
        transform.Translate(0, moveAmount, 0);
    }
}