using Animations;
using ScreensRoot;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "BackButtonSetting", menuName = "UI/HeaderFooterSettings/BackButtonSetting")]
    public class BackButtonSetting : HeaderFooterSetting
    {
        public ScreenName backToScreen;
        public ScreenTransitionDirection transition;

        public override void ApplySetting(AbstractScreenView screenView, ScreenNavigationSystem navigationSystem)
        {
            if (screenView.HeaderView.BackButton != null)
            {
                screenView.HeaderView.BackButton.gameObject.SetActive(true);
                screenView.HeaderView.BackButton.onClick.RemoveAllListeners();
                screenView.HeaderView.BackButton.onClick.AddListener(() =>
                {
                    navigationSystem.Show(backToScreen, transition);
                });
            }
        }
    }

    [CreateAssetMenu(fileName = "TitleSetting", menuName = "UI/HeaderFooterSettings/TitleSetting")]
    public class TitleSetting : HeaderFooterSetting
    {
        public string titleText;

        public override void ApplySetting(AbstractScreenView screenView, ScreenNavigationSystem navigationSystem)
        {
            if (screenView.HeaderView.Title != null)
            {
                screenView.HeaderView.Title.gameObject.SetActive(true);
                screenView.HeaderView.Title.text = titleText;
            }
        }
    }
}