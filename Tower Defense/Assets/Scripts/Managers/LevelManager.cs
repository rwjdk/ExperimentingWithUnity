using Enemies;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private int _numberOfLives;

        public int NumberOfLives => _numberOfLives;

        private void OnEnable()
        {
            Enemy.ReachedEnd += Enemy_ReachedEnd;
        }

        private void OnDisable()
        {
            Enemy.ReachedEnd -= Enemy_ReachedEnd;
        }

        private void Enemy_ReachedEnd(Enemy enemy)
        {
            _numberOfLives--;
            if (NumberOfLives == 0)
            {
                //todo - Game over
            }
        }
    }
}