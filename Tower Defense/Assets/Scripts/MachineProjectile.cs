using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineProjectile : Projectile
{
    public Vector2 Direction { get; set; }

    protected override void Update()
    {
        MoveProjectile();
    }

    protected override void MoveProjectile()
    {
        Vector2 movement = Direction.normalized * _moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        try
        {
            if (other.CompareTag(Constants.Tags.Enemy))
            {
                var enemy = other.GetComponent<Enemy>();
                if (enemy.Health.CurrentHealth > 0)
                {
                    enemy.Health.DealDamage(_damage);
                }
                ObjectPooler.ReturnToPool(gameObject);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(ObjectPooler.ReturnToPoolWithDelay(gameObject, 5f));
    }
}
