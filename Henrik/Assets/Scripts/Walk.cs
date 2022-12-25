using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Walk : MonoBehaviour
{
    private GameObject _marketTarget;
    private GameObject _stablesTarget;
    private Coroutine _r1;

    private void Start()
    {
        _marketTarget = GameObject.FindGameObjectWithTag("Market");
        _stablesTarget = GameObject.FindGameObjectWithTag("Stables");
        StartCoroutine(GoToMarket());
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.M))
        {
            GoToMarket();
        }
        
        if (Input.GetKeyDown(KeyCode.S)) 
        {
            GoToStables();
        }*/

        
    }

    /*
    private void MoveToTarget()
    {
        if (_target == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, 1*Time.deltaTime);

    }*/

    private IEnumerator GoToMarket()
    {
        yield return new WaitForSeconds(3);
        while (!IsTwoVectorPositionsCloseEnoughToBeConsideredEqual(transform.position, _marketTarget.transform.position))
        {
            yield return new WaitForSeconds(0.01f);
            transform.position = Vector3.MoveTowards(transform.position, _marketTarget.transform.position, 0.2f);
        }
        
        yield return new WaitForSeconds(5);

        while (!IsTwoVectorPositionsCloseEnoughToBeConsideredEqual(transform.position, _stablesTarget.transform.position))
        {
            yield return new WaitForSeconds(0.01f);
            transform.position = Vector3.MoveTowards(transform.position, _stablesTarget.transform.position, 0.2f);
        }
    }

    public static bool IsTwoVectorPositionsCloseEnoughToBeConsideredEqual(Vector3 firstVector, Vector3 secondVector)
    {
        return (firstVector - secondVector).magnitude < 0.01;
    }
}


