using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject _car;

    private void LateUpdate()
    {
        transform.position = _car.transform.position + MoveCameraInZSpace();
    }

    private static Vector3 MoveCameraInZSpace()
    {
        return new Vector3(0, 0, -200); //Needed because of the make-shift way camera is controlled in the lesson. A Cinemachine camera would fix this if it followed car
    }
}