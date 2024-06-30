using System;
using System.Collections.Generic;
using FirstScreen;
using SecondScreen;
using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    [Header("Root Objects")]
    [SerializeField] private Transform allScreensContainer;
    [SerializeField] private ScreenDatabase screenDatabase;
    [SerializeField] private ScreenName defaultScreenName;

    private readonly Dictionary<ScreenName, AbstractScreenView> _screens = new();
    private readonly Dictionary<AbstractScreenView, AbstractScreenController> _controllers = new();
    private ScreenNavigationSystem _screenNavigationSystem;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _screenNavigationSystem = new ScreenNavigationSystem(_screens, defaultScreenName);
        
        foreach (ScreenName screenName in Enum.GetValues(typeof(ScreenName)))
        {
            var screenView = screenDatabase[screenName];
            if (screenView != null)
            {
                var screenViewInstance = Instantiate(screenView, allScreensContainer);
                screenViewInstance.gameObject.SetActive(false);

                AbstractScreenController controller;
                switch (screenName)
                {
                    case ScreenName.First:
                        controller = new FirstScreenController(screenViewInstance as FirstScreenView, _screenNavigationSystem);
                        break;
                    case ScreenName.Second:
                        controller = new SecondScreenController(screenViewInstance as SecondScreenView, _screenNavigationSystem);
                        break;
                    default:
                        controller = new AbstractScreenController(screenViewInstance, _screenNavigationSystem);
                        break;
                }

                _screens.Add(screenName, screenViewInstance);
                _controllers.Add(screenViewInstance, controller);
            }
        }

        _screenNavigationSystem.InitControllers(_controllers);
        _screenNavigationSystem.ShowScreen(defaultScreenName);
    }
}
