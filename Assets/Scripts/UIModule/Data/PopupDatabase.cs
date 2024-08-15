using System.Collections.Generic;
using DefaultNamespace;
using ScreensRoot;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PopupDatabase", menuName = "ScriptableObjects/Popups/PopupDatabase", order = 102)]
    public class PopupDatabase : ScriptableObject
    {
        [SerializeField] private List<AbstractPopupView> popupViews = new();

        private Dictionary<PopupName, AbstractPopupView> _popupsDictionary;

        public AbstractPopupView this[PopupName popupName]
        {
            get
            {
                if (_popupsDictionary == null)
                    Init();
                return _popupsDictionary?.GetValueOrDefault(popupName);
            }
        }

        private void Init()
        {
            _popupsDictionary = new();

            foreach (var popupView in popupViews)
            {
                _popupsDictionary[popupView.PopupName] = popupView;
            }
        }
    }
}