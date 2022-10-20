using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject _car;

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        var carPosition = _car.transform.position;
        transform.position = _car.transform.position + new Vector3(0, 0, -10);
    }
}
