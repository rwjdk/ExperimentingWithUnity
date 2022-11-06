using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] private float fireRate = 02f;

    public bool isFireing;
    private Coroutine _fireringCoroutine;

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (isFireing && _fireringCoroutine == null)
        {
            _fireringCoroutine = StartCoroutine(FireContiniously());
        }
        else if (!isFireing && _fireringCoroutine != null)
        {
            StopCoroutine(_fireringCoroutine);
            _fireringCoroutine = null;
        }
    }

    private IEnumerator FireContiniously()
    {
        while (true)
        {
            var instantiate = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            var instanceRigidBody = instantiate.GetComponent<Rigidbody2D>();
            instanceRigidBody.velocity = transform.up * projectileSpeed;

            Destroy(instantiate, projectileLifeTime);
            yield return new WaitForSeconds(fireRate);
        }
    }
}
