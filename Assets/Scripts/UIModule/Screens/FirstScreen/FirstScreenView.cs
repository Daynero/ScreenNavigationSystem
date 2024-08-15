using ScreensRoot;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FirstScreen
{
    public class FirstScreenView : AbstractScreenView
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public Button ButtonPopup { get; private set; }
        [field: SerializeField] public TMP_InputField InputField { get; private set; }
        public override ScreenName ScreenName => ScreenName.First;
    }
}