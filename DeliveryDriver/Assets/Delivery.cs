using System;
using System.Diagnostics;
using Assets;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

// ReSharper disable FieldCanBeMadeReadOnly.Local

[UsedImplicitly]
public class Delivery : MonoBehaviour
{
    private TextMeshProUGUI _timer;
    private Stopwatch _stopWatch;

    private bool _hasPackage;

    [SerializeField] private GameObject _car;
    [SerializeField] private GameObject _carPackage;

    private SpriteRenderer _spriteRenderer;
    private Color32 _noPackageColor = Color.white;
    private Color32 _hasPackageColor = Color.green;

    [UsedImplicitly]
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
            case Tags.Package:
                if (!_hasPackage)
                {
                    PickUp(other);
                }
                else
                {
                    Debug.Log("You already have a package... Deliver that first");
                }
                
                break;
            case Tags.Customer:
                if (_hasPackage)
                {
                    Deliver();
                }
                else
                {
                    Debug.Log("You need to pick up a package first");
                }
                break;
            case Tags.Booster:
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
