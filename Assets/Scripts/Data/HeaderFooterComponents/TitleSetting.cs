using ScreensRoot;
using UnityEngine;

namespace Data.HeaderFooterComponents
{
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