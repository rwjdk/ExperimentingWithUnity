using System;
using System.Collections;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator _animator;
    private static readonly int Die = Animator.StringToHash(@"Die");
    private static readonly int Hurt = Animator.StringToHash(@"Hurt");
    private Enemy _enemy;
    private EnemyHealth _enemyHealth;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        EnemyHealth.Hurt += PlayHurt;
        EnemyHealth.Died += PlayDie;
    }

    private void PlayHurt(Enemy enemy)
    {
        if (enemy == _enemy)
        {
            StartCoroutine(PlayHurtAnimation());
        }
    }
    
    private void PlayDie(Enemy enemy)
    {
        if (enemy == _enemy)
        {
            StartCoroutine(PlayDieAnimation());
        }
    }

    private void OnDisable()
    {
        EnemyHealth.Hurt -= PlayHurt;
        EnemyHealth.Died -= PlayDie;
    }

    private IEnumerator PlayHurtAnimation()
    {
        _enemy.StopMovement();
        _animator.SetTrigger(Hurt);
        yield return new WaitForSeconds(GetCurrentAnimationLenght());
        _enemy.ResumeMovement();
    }

    private IEnumerator PlayDieAnimation()
    {
        _enemy.StopMovement();
        _animator.SetTrigger(Die);
        yield return new WaitForSeconds(GetCurrentAnimationLenght());
        //_enemy.ResumeMovement();
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(gameObject);
    }

    private float GetCurrentAnimationLenght()
    {
        return _animator.GetCurrentAnimatorClipInfo(0).Length;
    }
}