using System;
using Enemies;
using Shared;
using Spawners;
using UnityEngine;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField] private int _numberOfLives;

        public int NumberOfLives => _numberOfLives;
        public int CurrentWave { get; set; }

        private void Start()
        {
            CurrentWave = 1;
        }

        private void OnEnable()
        {
            Enemy.ReachedEnd += Enemy_ReachedEnd;
            Spawner.OnWaveCompleted += OnWaveCompleted;
        }

        private void OnWaveCompleted()
        {
            CurrentWave++;
        }

        private void OnDisable()
        {
            Enemy.ReachedEnd -= Enemy_ReachedEnd;
            Spawner.OnWaveCompleted -= OnWaveCompleted;
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