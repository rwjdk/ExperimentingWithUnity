using Logic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] private GameObject _carPackage;
    [SerializeField] private GameObject _customerThankYou;
    private SpriteRenderer _customerThankYouSpriteRenderer;
    private bool _hasPackage;
    private UiDisplay _uiDisplay;
    private Movement _movement;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _customerThankYouSpriteRenderer = _customerThankYou.GetComponent<SpriteRenderer>();
        _uiDisplay = FindObjectOfType<UiDisplay>();

        ShowHideThankYou(false);
        ShowBoxInCar(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.Package))
        {
            PickUp(other);
        }
        else if (other.CompareTag(Constants.Tags.Customer))
        {
            Deliver();
        }
        else if (other.CompareTag(Constants.Tags.Booster))
        {
            Boost(other);
        }
    }

    private void Boost(Collider2D other)
    {
        _movement.IsBoosted = true;
        Destroy(other.gameObject);
    }

    private void PickUp(Collider2D other)
    {
        if (!_hasPackage)
        {
            _hasPackage = true;
            Debug.Log("You picked up a package");
            Destroy(other.gameObject);
            ShowBoxInCar(true);
        }
        else
        {
            Debug.Log("You already have a package... Deliver that first");
        }
    }

    private void Deliver()
    {
        if (_hasPackage)
        {
            _hasPackage = false;
            Debug.Log("You delivered a package");
            ShowBoxInCar(false);
            ShowHideThankYou(true);
            Invoke(nameof(HideThankYouOnDelay), 3); //CoRoutines are better to use but learned this first so keeping it here
            CheckForVictoryCondition();
        }
        else
        {
            Debug.Log("You need to pick up a package first");
        }
    }

    public void HideThankYouOnDelay()
    {
        ShowHideThankYou(false);
    }

    private void ShowHideThankYou(bool show)
    {
        _customerThankYouSpriteRenderer.enabled = show;
    }

    private void CheckForVictoryCondition()
    {
        var numberOfPackagesLeft = GameObject.FindGameObjectsWithTag(Constants.Tags.Package).Length;
        if (numberOfPackagesLeft == 0)
        {
            _uiDisplay.StopTimerAndShowWonText();
        }
    }

    private void ShowBoxInCar(bool value)
    {
        _carPackage.SetActive(value);
    }
}