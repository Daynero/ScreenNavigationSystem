using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ScreenConfiguration", menuName = "UI/ScreenConfiguration")]
    public class ScreenConfiguration : ScriptableObject
    {
        public List<HeaderFooterSetting> headerSettings;
        public List<HeaderFooterSetting> footerSettings;
    }
}