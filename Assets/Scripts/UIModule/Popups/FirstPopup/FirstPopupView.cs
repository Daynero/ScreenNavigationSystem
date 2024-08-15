using UIModule.BaseViewAndControllers;
using UnityEngine;
using UnityEngine.UI;

namespace UIModule.Popups.FirstPopup
{
    public class FirstPopupView : AbstractPopupView
    {
        [field: SerializeField] public Button Button { get; private set; }

        public override PopupName PopupName => PopupName.First;
    }
}