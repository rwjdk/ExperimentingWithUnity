using System;
using System.Diagnostics;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

// ReSharper disable FieldCanBeMadeReadOnly.Local

[UsedImplicitly]
public class Driver : MonoBehaviour
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    [SerializeField]
    private float _steerSpeed = 150;
    [SerializeField]
    private float _moveSpeed = 5;
    
    [SerializeField]
    public bool IsBoosted = false;

    void Start()
    {
        }

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