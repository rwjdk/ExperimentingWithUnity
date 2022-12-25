using UnityEngine;

public class Ground : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
    }

    public void DamageGround()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }
}
