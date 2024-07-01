using System.Collections.Generic;
using ScreensRoot;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PanelDatabase", menuName = "ScriptableObjects/Panels/PanelDatabase", order = 101)]
    public class ScreenDatabase : ScriptableObject
    {
        [SerializeField] private List<AbstractScreenView> screenViews = new();

        private Dictionary<ScreenName, AbstractScreenView> _panelsDictionary;

        public AbstractScreenView this[ScreenName screenName]
        {
            get
            {
                if (_panelsDictionary == null)
                    Init();
                return _panelsDictionary?.GetValueOrDefault(screenName);
            }
        }

        private void Init()
        {
            _panelsDictionary = new();
            
            foreach (var panelMono in screenViews)
            {
                _panelsDictionary[panelMono.ScreenName] = panelMono;
            }
        }
    }
}