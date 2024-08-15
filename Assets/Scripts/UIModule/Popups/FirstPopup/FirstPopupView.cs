using DefaultNamespace;
using ScreensRoot;
using UnityEngine;
using UnityEngine.UI;

namespace FirstPopup
{
    public class FirstPopupView : AbstractPopupView
    {
        [field: SerializeField] public Button Button { get; private set; }

        public override PopupName PopupName => PopupName.First;
    }
}