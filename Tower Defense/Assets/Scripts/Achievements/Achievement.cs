using Managers;
using Shared;
using UnityEngine;

namespace Achievements
{
    [CreateAssetMenu(fileName = "New Achievement", menuName = "Achievement")]
    public class Achievement : ScriptableObject
    {
        [SerializeField] private AchivementId _id;
        [SerializeField] private string _title;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _progressToUnlock;
        [SerializeField] private int _goldReward;

        public bool Unlocked => CurrentProgress>=_progressToUnlock;

        public int CurrentProgress;

        public AchivementId Id => _id;

        public string Title => _title;

        public Sprite Sprite => _sprite;

        public int ProgressToUnlock => _progressToUnlock;

        public int GoldReward => _goldReward;

        public void AddProgress(int amountToAdvance)
        {
            CurrentProgress += amountToAdvance;
            AchievementManager.AchievementProgressed?.Invoke(this);
            if (Unlocked)
            {
                AchievementManager.AchievementUnlocked?.Invoke(this);
            }
        }
    }
}