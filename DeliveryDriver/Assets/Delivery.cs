using JetBrains.Annotations;
using UnityEngine;
// ReSharper disable FieldCanBeMadeReadOnly.Local

[UsedImplicitly]
public class Delivery : MonoBehaviour
{
    private bool _hasPackage;

    [SerializeField] private GameObject _car;
    [SerializeField] private GameObject _carPackage;

    private SpriteRenderer _spriteRenderer;
    private Color32 _noPackageColor = Color.white;
    private Color32 _hasPackageColor = Color.green;

    [UsedImplicitly]
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material.color = _noPackageColor;
        ShowBoxInCar(false);
    }

    [UsedImplicitly]
    private void OnCollisionEnter2D(Collision2D other)
    {
        _car.GetComponent<Driver>().IsBoosted = false;
        Debug.Log("Hit the Obstacle");
    }

    
    [UsedImplicitly]
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Package":
                if (!_hasPackage)
                {
                    PickUp(other);
                }
                else
                {
                    Debug.Log("You already have a package... Deliver that first");
                }
                
                break;
            case "Customer":
                if (_hasPackage)
                {
                    Deliver();
                }
                else
                {
                    Debug.Log("You need to pick up a package first");
                }
                break;
            case "Booster":
                _car.GetComponent<Driver>().IsBoosted = true;
                Destroy(other.gameObject);
                break;
        }

        
    }

    private void PickUp(Collider2D other)
    {
        Debug.Log("You picked up a package");
        _hasPackage = true;
        ChangeColor(_hasPackageColor);
        Destroy(other.gameObject);
        ShowBoxInCar(true);
    }

    private void ChangeColor(Color32 newColor)
    {
        /*
        _spriteRenderer.color = newColor;
        */
    }

    private void Deliver()
    {
        Debug.Log("You delivered a package");
        _hasPackage = false;
        ChangeColor(_noPackageColor);
        ShowBoxInCar(false);
    }

    private void ShowBoxInCar(bool value)
    {
        _carPackage.SetActive(value);
    }

    /*
    [UsedImplicitly]
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Moved out of the trigger: " + other.name);
    }*/
}
