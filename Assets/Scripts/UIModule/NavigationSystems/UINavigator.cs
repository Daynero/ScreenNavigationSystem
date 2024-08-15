using System;
using System.Collections.Generic;
using UIModule.Animations;
using UIModule.BaseViewAndControllers;
using UIModule.BottomSheets;
using UIModule.Core;
using UIModule.Popups;
using UIModule.Screens;

namespace UIModule.NavigationSystems
{
    public class UINavigator : IUINavigator
    {
        private readonly Dictionary<ScreenName, AbstractScreenView> _screens;
        private readonly Dictionary<PopupName, AbstractPopupView> _popups;
        private readonly Dictionary<BottomSheetName, AbstractBottomSheetView> _sheets;
        private readonly ScreenName _defaultScreenName;

        private static UINavigator _instance;
        private static ScreenNavigationSystem _screenNavigationSystem;
        private static PopupNavigationSystem _popupNavigationSystem;
        private static BottomSheetNavigationSystem _bottomSheetNavigationSystem;

        private AbstractScreenView _currentScreen;
        private AbstractPopupView _currentPopup;
        private AbstractBottomSheetView _currentBottomSheet;

        public static UINavigator Instance =>
            _instance ?? throw new InvalidOperationException("UINavigator is not initialized.");

        public UINavigator(Dictionary<ScreenName, AbstractScreenView> screens,
            Dictionary<PopupName, AbstractPopupView> popups, Dictionary<BottomSheetName, AbstractBottomSheetView> sheets,
            ScreenName defaultScreenName)
        {
            _screens = screens;
            _popups = popups;
            _sheets = sheets;
            _defaultScreenName = defaultScreenName;
            _instance = this;

            InitNavigationsSystems();
        }

        private void InitNavigationsSystems()
        {
            _screenNavigationSystem = new ScreenNavigationSystem(_screens, _defaultScreenName);
            _popupNavigationSystem = new PopupNavigationSystem(_popups);
            _bottomSheetNavigationSystem = new BottomSheetNavigationSystem();
        }

        public void InitScreenNavigation(Dictionary<AbstractScreenView, AbstractScreenController> screenControllers,
            Action<ScreenName> onScreenMissing)
        {
            _screenNavigationSystem.InitControllers(screenControllers);
            _screenNavigationSystem.OnScreenMissing += onScreenMissing;
        }

        public IUINavigator Show(ScreenName screenName, ScreenTransitionType transitionType = ScreenTransitionType.None)
        {
            _currentScreen = _screenNavigationSystem.Show(screenName, transitionType);
            return this;
        }

        public IUINavigator Show(PopupName popupName, PopupTransitionType transitionType = PopupTransitionType.None)
        {
            _currentPopup = _popupNavigationSystem.Show(popupName, transitionType);
            return this;
        }

        public IUINavigator Show(BottomSheetName bottomSheetName)
        {
            _currentBottomSheet = _bottomSheetNavigationSystem.Show(bottomSheetName);
            return this;
        }

        public IUINavigator With<T>(T data) where T : BaseVm
        {
            if (_currentScreen != null)
            {
                _screenNavigationSystem.ShowWithData(_currentScreen.ScreenName, data);
            }

            if (_currentPopup != null)
            {
                _popupNavigationSystem.ShowWithData(_currentPopup.PopupName, data);
            }

            if (_currentBottomSheet != null)
            {
                _bottomSheetNavigationSystem.ShowWithData(_currentBottomSheet.BottomSheetName, data);
            }

            return this;
        }

        public void CloseAll(UIType type = UIType.All)
        {
            if (type == UIType.All || type == UIType.Screens)
            {
                _screenNavigationSystem.HideAllViews();
                _currentScreen = null;
            }

            if (type == UIType.All || type == UIType.Popups)
            {
                _popupNavigationSystem.CloseAll();
                _currentPopup = null;
            }

            if (type == UIType.All || type == UIType.BottomSheets)
            {
                _bottomSheetNavigationSystem.CloseAll();
                _currentBottomSheet = null;
            }
        }

        public void InitPopupNavigation(Dictionary<AbstractPopupView,AbstractPopupController> popupControllers, Action<PopupName> createPopup)
        {
            _popupNavigationSystem.InitControllers(popupControllers);
            _popupNavigationSystem.OnPopupMissing += createPopup;
        }

        public void InitBottomSheetNavigation(Dictionary<AbstractBottomSheetView,AbstractBottomSheetController> sheetControllers, Action<BottomSheetName> createBottomSheet)
        {
        
        }
    }
}
