using System;
using System.Linq;
using Achievements;
using Shared;
using UnityEngine;

namespace Managers
{
    public class AchievementManager : Singleton<AchievementManager>
    {
        public static Action<Achievement> AchievementProgressed;
        public static Action<Achievement> AchievementUnlocked;
        public static Action ForceRefresh;

        [SerializeField] private AchievementCard _achievementCardPrefab;
        [SerializeField] private Transform _achievementContainer;
        [SerializeField] private Achievement[] _achievements;

        private void Start()
        {
            foreach (var achievement in _achievements)
            {
                achievement.CurrentProgress = 0;
                LoadAchievement(achievement);
            }
        }

        private void LoadAchievement(Achievement achievement)
        {
            var instance = Instantiate(_achievementCardPrefab, _achievementContainer);
            instance.SetupAchievement(achievement);
        }

        public void AddProgress(int amountToProgress, params AchivementId[] idsToAdvance)
        {
            foreach (var achievementId in idsToAdvance)
            {
                var achievement = _achievements.FirstOrDefault(x => x.Id == achievementId);
                if (achievement != null)
                {
                    achievement.AddProgress(amountToProgress);
                }
            }
        }

        public void UpdateAllProgress()
        {
            ForceRefresh?.Invoke();
        }
    }
}