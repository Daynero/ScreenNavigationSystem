using System;
using System.Collections.Generic;
using Data;
using DefaultNamespace;
using FirstScreen;
using ScreensRoot;
using UnityEngine;

public class UICompositionRoot : MonoBehaviour
{
    [Header("Root Objects")] 
    [SerializeField] private Transform allScreensContainer;
    [SerializeField] private ScreenDatabase screenDatabase;
    [SerializeField] private ScreenName defaultScreenName;

    private readonly Dictionary<ScreenName, AbstractScreenView> _screens = new();
    private readonly Dictionary<PopupName, AbstractPopupView> _popups = new();
    private readonly Dictionary<BottomSheetName, AbstractBottomSheetView> _sheets = new();
    private readonly Dictionary<AbstractPopupView, AbstractPopupController> _popupControllers = new();
    private readonly Dictionary<AbstractScreenView, AbstractScreenController> _screenControllers = new();
    private readonly Dictionary<AbstractBottomSheetView, AbstractBottomSheetController> _sheetControllers = new();
    private UINavigator _uiNavigator;
    private ScreenControllerFactory _screenControllerFactory;
    private PopupControllerFactory _popupControllerFactory;
    private BottomSheetControllerFactory _bottomSheetControllerFactory;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _uiNavigator = new UINavigator(_screens, _popups, _sheets, defaultScreenName);
        _screenControllerFactory = new ();
        _popupControllerFactory = new ();
        _bottomSheetControllerFactory = new ();

        CreateScreen(defaultScreenName);
        _uiNavigator.InitScreenNavigation(_screenControllers, CreateScreen);
    }

    private void CreateScreen(ScreenName screenName)
    {
        var screenView = screenDatabase[screenName];
        if (screenView != null)
        {
            var screenViewInstance = Instantiate(screenView, allScreensContainer);
            screenViewInstance.gameObject.SetActive(false);

            AbstractScreenController controller = _screenControllerFactory.CreateController(screenViewInstance,
                _uiNavigator);

            _screens.Add(screenName, screenViewInstance);
            _screenControllers.Add(screenViewInstance, controller);
        }
    }
}