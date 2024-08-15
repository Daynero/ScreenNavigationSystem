using TMPro;
using UIModule.BaseViewAndControllers;
using UnityEngine;
using UnityEngine.UI;

namespace UIModule.Screens.SecondScreen
{
    public class SecondScreenView : AbstractScreenView
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TMP_Text Text { get; private set; }
        public override ScreenName ScreenName => ScreenName.Second;
    }
}