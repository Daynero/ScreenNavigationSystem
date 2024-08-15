using UIModule.BaseViewAndControllers;
using UnityEngine;
using UnityEngine.UI;

namespace UIModule.Popups.SecondPopup
{
    public class SecondPopupView : AbstractPopupView
    {
        [field: SerializeField] public Button Button { get; private set; }

        public override PopupName PopupName => PopupName.Second;
    }
}