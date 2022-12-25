using UnityEngine;

public class Missile : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position += Vector3.right * Time.deltaTime;
    }
}
