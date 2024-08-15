using System.Collections.Generic;
using UIModule.BaseViewAndControllers;
using UIModule.BottomSheets;
using UnityEngine;

namespace UIModule.Data
{
    [CreateAssetMenu(fileName = "BottomSheetDatabase", menuName = "ScriptableObjects/BottomSheets/BottomSheetDatabase", order = 103)]
    public class BottomSheetDatabase : ScriptableObject
    {
        [SerializeField] private List<AbstractBottomSheetView> bottomSheetViews = new();

        private Dictionary<BottomSheetName, AbstractBottomSheetView> _bottomSheetsDictionary;

        public AbstractBottomSheetView this[BottomSheetName bottomSheetName]
        {
            get
            {
                if (_bottomSheetsDictionary == null)
                    Init();
                return _bottomSheetsDictionary?.GetValueOrDefault(bottomSheetName);
            }
        }

        private void Init()
        {
            _bottomSheetsDictionary = new();

            foreach (var bottomSheetView in bottomSheetViews)
            {
                _bottomSheetsDictionary[bottomSheetView.BottomSheetName] = bottomSheetView;
            }
        }
    }
}