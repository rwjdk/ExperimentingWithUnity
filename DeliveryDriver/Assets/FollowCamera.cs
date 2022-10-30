using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject _car;

    void LateUpdate()
    {
        transform.position = _car.transform.position + new Vector3(0, 0, -10);
    }
}
