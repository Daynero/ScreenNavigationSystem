using System;
using System.Collections.Generic;
using Animations;
using FirstScreen;
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

        public void Show(ScreenName screenName,
            ScreenTransitionDirection transitionDirection = ScreenTransitionDirection.None)
        {
            if (screenName == ScreenName.None)
            {
                CloseCurrentScreen();
                return;
            }

            if (!IsScreenAvailable(screenName)) 
                return;
        
            SwitchScreen(screenName, transitionDirection);
        }

        public void ShowWithData<T>(ScreenName screenName, T data,
            ScreenTransitionDirection transitionDirection = ScreenTransitionDirection.None) where T:BaseVm
        {
            if (screenName == ScreenName.None)
            {
                CloseCurrentScreen();
                return;
            }
            
            if (!IsScreenAvailable(screenName)) 
                return;
        
            var nextScreen = SwitchScreen(screenName, transitionDirection);
            _controllers[nextScreen].ShowWithData<BaseVm>(data);
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

        private AbstractScreenView SwitchScreen(ScreenName screenName, ScreenTransitionDirection transitionDirection)
        {
            AbstractScreenView currentScreen = _screens[_currentScreenName];
            AbstractScreenView nextScreen = _screens[screenName];
        
            if (transitionDirection != ScreenTransitionDirection.None)
            {
                var animationController = new ScreenAnimationController(currentScreen, nextScreen, transitionDirection);
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

        public void HideAllViews()
        {
            foreach (var controller in _controllers)
            {
                controller.Value.Hide();
            }
        }
    }
