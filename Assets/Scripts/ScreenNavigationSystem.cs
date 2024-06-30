using System.Collections.Generic;
using Animations;
using UnityEngine;

public class ScreenNavigationSystem
{
    private Dictionary<ScreenName, AbstractScreenView> _screens;
    private Dictionary<AbstractScreenView, AbstractScreenController> _controllers;
    private ScreenName _currentScreenName;

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
        if (!IsScreenAvailable(screenName)) 
            return;
        
        SwitchScreen(screenName, transitionDirection);
    }

    public void ShowWithData(ScreenName screenName, object data,
        ScreenTransitionDirection transitionDirection = ScreenTransitionDirection.None)
    {
        if (!IsScreenAvailable(screenName)) 
            return;
        
        var nextScreen = SwitchScreen(screenName, transitionDirection);
        _controllers[nextScreen].ShowWithData(data);
    }

    private bool IsScreenAvailable(ScreenName screenName)
    {
        if (_screens.ContainsKey(screenName)) return true;
        
        Debug.LogError($"Screen name {screenName} not found in screens.");
        return false;

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
            currentScreen.gameObject.SetActive(false);
            nextScreen.gameObject.SetActive(true);
        }
        
        _currentScreenName = nextScreen.ScreenName;

        return nextScreen;
    }
}