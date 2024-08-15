using System;
using System.Collections.Generic;
using Animations;
using ScreensRoot;
using UnityEngine;

    public class ScreenNavigationSystem
    {
        private readonly Dictionary<ScreenName, AbstractScreenView> _screens;
        private Dictionary<AbstractScreenView, AbstractScreenController> _controllers;
        private ScreenName _currentScreenName;
        public event Action<ScreenName> OnScreenMissing;

        public ScreenNavigationSystem(Dictionary<ScreenName, AbstractScreenView> screens, ScreenName initialScreenName)
        {
            _screens = screens;
            _currentScreenName = initialScreenName;
        }

        public void InitControllers(Dictionary<AbstractScreenView, AbstractScreenController> controllers) =>
            _controllers = controllers;

        public AbstractScreenView Show(ScreenName screenName,
            ScreenTransitionType transitionType = ScreenTransitionType.None)
        {
            if (screenName == ScreenName.None)
            {
                CloseCurrentScreen();
                return null;
            }

            if (!IsScreenAvailable(screenName)) 
                return null;

            return SwitchScreen(screenName, transitionType);
        }
        //
        // public void ShowWithData<T>(ScreenName screenName, T data,
        //     ScreenTransitionType transitionType = ScreenTransitionType.None) where T:BaseVm
        // {
        //     if (screenName == ScreenName.None)
        //     {
        //         CloseCurrentScreen();
        //         return;
        //     }
        //     
        //     if (!IsScreenAvailable(screenName)) 
        //         return;
        //
        //     var nextScreen = SwitchScreen(screenName, transitionType);
        //     _controllers[nextScreen].ShowWithData<BaseVm>(data);
        // }
        
        public AbstractScreenView Show(ScreenName screenName)
        {
            if (screenName == ScreenName.None)
            {
                CloseCurrentScreen();
                return null;
            }

            if (!IsScreenAvailable(screenName)) 
                return null;

            return SwitchScreen(screenName);
        }

        public void ShowWithData<T>(ScreenName screenName, T data) where T : BaseVm
        {
            if (_screens.TryGetValue(screenName, out var nextScreen))
            {
                _controllers[nextScreen].ShowWithData(data);
            }
        }

        private void CloseCurrentScreen()
        {
            _controllers[_screens[_currentScreenName]].Hide();
        }

        private bool IsScreenAvailable(ScreenName screenName)
        {
            CheckAndTryCreateScreen(screenName);
            
            if (_screens.ContainsKey(screenName)) return true;
        
            Debug.LogError($"Screen name {screenName} not found in screens.");
            return false;
        }

        private void CheckAndTryCreateScreen(ScreenName screenName)
        {
            if (!_screens.ContainsKey(screenName))
            {
                OnScreenMissing?.Invoke(screenName);
            }
        }

        private AbstractScreenView SwitchScreen(ScreenName screenName, ScreenTransitionType transitionType)
        {
            AbstractScreenView currentScreen = _screens[_currentScreenName];
            
            if (_currentScreenName == screenName)
            {
                _controllers[currentScreen].Show();
                return _screens[_currentScreenName];
            }

            AbstractScreenView nextScreen = _screens[screenName];

            if (transitionType != ScreenTransitionType.None)
            {
                var animationController = new ScreenAnimationController(currentScreen, nextScreen, transitionType);
                animationController.PlayAnimation();
            }
            else
            {
                _controllers[currentScreen].Hide();
                _controllers[nextScreen].Show();
            }
        
            _currentScreenName = nextScreen.ScreenName;

            return nextScreen;
        }
        
        private AbstractScreenView SwitchScreen(ScreenName screenName)
        {
            AbstractScreenView currentScreen = _screens[_currentScreenName];
            AbstractScreenView nextScreen = _screens[screenName];
        
            _controllers[currentScreen].Hide();
            _controllers[nextScreen].Show();
        
            _currentScreenName = nextScreen.ScreenName;

            return nextScreen;
        }

        public void HideAllViews()
        {
            foreach (var controller in _controllers)
            {
                controller.Value.Hide();
            }
        }
    }
