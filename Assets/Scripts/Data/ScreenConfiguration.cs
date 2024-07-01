using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ScreenConfiguration", menuName = "UI/ScreenConfiguration")]
    public class ScreenConfiguration : ScriptableObject
    {
        public bool showHeader;
        public bool showFooter;
        public string headerTitle;
        public ScreenName backToScreen = ScreenName.None;
    }
}