using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class First : AbstractPanelMono
    {
        [field: SerializeField] public Button Button { get; private set; }
        public override PanelType PanelType => PanelType.First;
    }
}