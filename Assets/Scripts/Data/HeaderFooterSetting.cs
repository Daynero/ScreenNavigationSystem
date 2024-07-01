using ScreensRoot;
using UnityEngine;

namespace Data
{
    public abstract class HeaderFooterSetting : ScriptableObject
    {
        public abstract void ApplySetting(AbstractScreenView screenView, ScreenNavigationSystem navigationSystem);
    }
}