using DefaultNamespace;
using ScreensRoot;
using UnityEngine;
using UnityEngine.UI;

namespace SecondPopup
{
    public class SecondPopupView : AbstractPopupView
    {
        [field: SerializeField] public Button Button { get; private set; }

        public override PopupName PopupName => PopupName.Second;
    }
}