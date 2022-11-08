using UnityEngine;

public class Collision : MonoBehaviour
{
    private void OnCollisionEnter2D() //In a real game one should check what was hit
    {
        gameObject.GetComponent<Movement>().IsBoosted = false;
        Debug.Log("Hit the Obstacle");
    }
}