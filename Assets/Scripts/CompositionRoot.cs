using System;
using System.Collections.Generic;
using Data;
using FirstScreen;
using ScreensRoot;
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
    private RegistrationStateManager _registrationStateManager;
    private ScreenControllerFactory _screenControllerFactory;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _screenNavigationSystem = new ScreenNavigationSystem(_screens, defaultScreenName);
        _registrationStateManager = new RegistrationStateManager(_screenNavigationSystem, defaultScreenName);
        _screenControllerFactory = new ScreenControllerFactory();

        CreateScreen(defaultScreenName);
        _screenNavigationSystem.InitControllers(_controllers);
        _screenNavigationSystem.OnScreenMissing += CreateScreen;
        _registrationStateManager.LoadData();
    }

    private void CreateScreen(ScreenName screenName)
    {
        var screenView = screenDatabase[screenName];
        if (screenView != null)
        {
            var screenViewInstance = Instantiate(screenView, allScreensContainer);
            screenViewInstance.gameObject.SetActive(false);

            AbstractScreenController controller = _screenControllerFactory.CreateController(screenViewInstance,
                _screenNavigationSystem, _registrationStateManager);

            _screens.Add(screenName, screenViewInstance);
            _controllers.Add(screenViewInstance, controller);
        }
    }
}
