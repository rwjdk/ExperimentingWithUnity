using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Delivery : MonoBehaviour
{
    private TextMeshProUGUI _timer;
    private Stopwatch _stopWatch;

    private bool _hasPackage;

    [SerializeField] GameObject _car;
    [SerializeField] GameObject _carPackage;

    private SpriteRenderer _spriteRenderer;
    private readonly Color32 _noPackageColor = Color.white;

    void Start()
    {
        GameObject finishText = GameObject.FindGameObjectWithTag("FinishText");
        finishText.GetComponent<TextMeshProUGUI>().enabled = false;
        GameObject.FindGameObjectWithTag("ThankYou").GetComponent<SpriteRenderer>().enabled = false;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material.color = _noPackageColor;
        ShowBoxInCar(false);

        _stopWatch = Stopwatch.StartNew();
        var findGameObjectWithTag = GameObject.FindGameObjectWithTag("Timer");
        _timer = findGameObjectWithTag.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //Update Timer
        _timer.text = Math.Round(_stopWatch.Elapsed.TotalSeconds, 0) + " Seconds";
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _car.GetComponent<Driver>().IsBoosted = false;
        Debug.Log("Hit the Obstacle");
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Tags.Package))
        {
            if (!_hasPackage)
            {
                PickUp(other);
            }
            else
            {
                Debug.Log("You already have a package... Deliver that first");
            }
        }
        else if (other.CompareTag(Tags.Customer))
        {
            if (_hasPackage)
            {
                Deliver();
            }
            else
            {
                Debug.Log("You need to pick up a package first");
            }
        }
        else if (other.CompareTag(Tags.Booster))
        {
            _car.GetComponent<Driver>().IsBoosted = true;
            Destroy(other.gameObject);
        }
    }

    private void PickUp(Collider2D other)
    {
        Debug.Log("You picked up a package");
        _hasPackage = true;
        Destroy(other.gameObject);
        ShowBoxInCar(true);
    }

    private void Deliver()
    {
        Debug.Log("You delivered a package");
        _hasPackage = false;
        ShowBoxInCar(false);
        ShowHideThankYou(true);
        Invoke(nameof(HideThankYouOnDelay), 3);
        CheckForVictoryCondition();
    }

    public void HideThankYouOnDelay()
    {
        ShowHideThankYou(false);
    }

    private static void ShowHideThankYou(bool show)
    {
        GameObject.FindGameObjectWithTag("ThankYou").GetComponent<SpriteRenderer>().enabled = show;
    }

    private void CheckForVictoryCondition()
    {
        var numberOfPackagesLeft = GameObject.FindGameObjectsWithTag(Tags.Package).Length;
        bool isWon = numberOfPackagesLeft == 0;
        if (isWon)
        {
            GameObject finishText = GameObject.FindGameObjectWithTag("FinishText");
            var textMeshProUgui = finishText.GetComponent<TextMeshProUGUI>();
            _stopWatch.Stop();
            textMeshProUgui.text =  $"Good Job, You won the game in {Math.Round(_stopWatch.Elapsed.TotalSeconds, 0)} Seconds!";
            textMeshProUgui.enabled = true;
            Debug.Log("You Won the game!");
        }
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
