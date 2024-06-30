using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SecondScreen
{
    public class SecondScreenView : AbstractScreenView
    {
        [field: SerializeField] public Button Button { get; private set; }
        [field: SerializeField] public TMP_Text Text { get; private set; }
        public override ScreenName ScreenName => ScreenName.Second;
    }
}