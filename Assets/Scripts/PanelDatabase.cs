using System.Collections.Generic;
using UnityEngine;

namespace PanelsNavigationModule
{
    [CreateAssetMenu(fileName = "PanelDatabase", menuName = "ScriptableObjects/Panels/PanelDatabase", order = 101)]
    public class PanelDatabase : ScriptableObject
    {
        [SerializeField] private List<AbstractPanelMono> screenViews = new();

        private Dictionary<PanelType, AbstractPanelMono> _panelsDictionary;

        public AbstractPanelMono this[PanelType panelType]
        {
            get
            {
                if (_panelsDictionary == null)
                    Init();
                return _panelsDictionary?.GetValueOrDefault(panelType);
            }
        }

        private void Init()
        {
            _panelsDictionary = new();
            
            foreach (var panelMono in screenViews)
            {
                _panelsDictionary[panelMono.PanelType] = panelMono;
            }
        }
    }
}