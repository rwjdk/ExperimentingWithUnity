using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    private Enemy _target;

    private void Update()
    {
        MoveProjectile();
        Rotate();
    }

    public void SetEnemy(Enemy enemy)
    {
        _target = enemy;
    }

    private void Rotate()
    {
        if (_target == null)
        {
            return;
        }

        var selfTransform = transform;
        var targetPosition = _target.transform.position - selfTransform.position;
        float angle = Vector3.SignedAngle(selfTransform.up, targetPosition, selfTransform.forward);
        transform.Rotate(0f, 0f, angle);

    }

    private void MoveProjectile()
    {
        if (_target == null)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
    }
}