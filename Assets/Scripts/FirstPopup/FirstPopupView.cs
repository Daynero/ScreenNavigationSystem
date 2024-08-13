using ScreensRoot;
using UnityEngine;
using UnityEngine.UI;

namespace FirstPopup
{
    public class FirstPopupView : AbstractScreenView
    {
        [field: SerializeField] public Button Button { get; private set; }
        public override ScreenName ScreenName => ScreenName.FirstPopup;
    }
}