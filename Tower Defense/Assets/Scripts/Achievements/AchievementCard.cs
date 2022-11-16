using System.Globalization;
using JetBrains.Annotations;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Achievements
{
    public class AchievementCard : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _progressText;
        [SerializeField] private TextMeshProUGUI _rewardAmountText;
        [SerializeField] private Button _rewardButton;

        private Achievement _achievementStoredInThisCard;

        public void SetupAchievement(Achievement achievement)
        {
            _achievementStoredInThisCard = achievement;
            _icon.sprite = achievement.Sprite;
            _titleText.text = achievement.Title;
            _progressText.text = GetProgressDisplayText(achievement);
            _rewardAmountText.text = achievement.GoldReward.ToString(CultureInfo.InvariantCulture);
        }

        [UsedImplicitly]
        public void ClaimReward()
        {
            if (_achievementStoredInThisCard.Unlocked)
            {
                CurrencyManager.Instance.AddCoins(_achievementStoredInThisCard.GoldReward);
                _rewardButton.gameObject.SetActive(false);
            }
        }

        private void OnEnable()
        {
            AchievementManager.AchievementProgressed += AchievementProgressed;
            AchievementManager.AchievementUnlocked += AchievementUnlocked;
            AchievementManager.ForceRefresh += ForceRefresh;
        }

        private void AchievementUnlocked(Achievement achievementThatUnlocked)
        {
            if (achievementThatUnlocked == _achievementStoredInThisCard)
            {
                RefreshUnlockedGui();
            }
        }

        private void RefreshUnlockedGui()
        {
            if (_achievementStoredInThisCard.Unlocked)
            {
                _rewardButton.interactable = true;
                gameObject.GetComponent<Image>().color = new Color(0.2f, 0.27f, 0.2f);
            }
        }

        private void OnDisable()
        {
            AchievementManager.AchievementProgressed -= AchievementProgressed;
            AchievementManager.AchievementUnlocked -= AchievementUnlocked;
            AchievementManager.ForceRefresh += ForceRefresh;
        }

        private void ForceRefresh()
        {
            RefreshProgressGui();
            RefreshUnlockedGui();
        }

        private void AchievementProgressed(Achievement achievementThatProgressed)
        {
            if (achievementThatProgressed == _achievementStoredInThisCard)
            {
                RefreshProgressGui();
            }
        }

        private void RefreshProgressGui()
        {
            _progressText.text = GetProgressDisplayText(_achievementStoredInThisCard);
        }

        private static string GetProgressDisplayText(Achievement achievement)
        {
            return achievement.CurrentProgress > achievement.ProgressToUnlock ? 
                $"{achievement.ProgressToUnlock}/{achievement.ProgressToUnlock}" : 
                $"{achievement.CurrentProgress}/{achievement.ProgressToUnlock}";
        }
    }
}