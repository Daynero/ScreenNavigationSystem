using System.Collections.Generic;
using Data;
using DefaultNamespace;
using FirstScreen;
using ScreensRoot;
using UnityEngine;

public class UICompositionRoot : MonoBehaviour
{
    [Header("Root Objects")] 
    [SerializeField] private Transform screenContainer;
    [SerializeField] private Transform popupContainer;
    [SerializeField] private Transform bottomSheetContainer;
    [SerializeField] private ScreenDatabase screenDatabase;
    [SerializeField] private PopupDatabase popupDatabase;
    [SerializeField] private BottomSheetDatabase bottomSheetDatabase;
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
        _screenControllerFactory = new ScreenControllerFactory();
        _popupControllerFactory = new PopupControllerFactory();
        _bottomSheetControllerFactory = new BottomSheetControllerFactory();

        CreateScreen(defaultScreenName);
        _uiNavigator.InitScreenNavigation(_screenControllers, CreateScreen);
        _uiNavigator.InitPopupNavigation(_popupControllers, CreatePopup);
        _uiNavigator.InitBottomSheetNavigation(_sheetControllers, CreateBottomSheet);

        _uiNavigator.Show(defaultScreenName);
    }

    private void CreateScreen(ScreenName screenName)
    {
        var screenView = screenDatabase[screenName];
        if (screenView != null)
        {
            var screenViewInstance = Instantiate(screenView, screenContainer);
            screenViewInstance.gameObject.SetActive(false);

            AbstractScreenController controller = _screenControllerFactory.CreateController(screenViewInstance, _uiNavigator);

            _screens.Add(screenName, screenViewInstance);
            _screenControllers.Add(screenViewInstance, controller);
        }
    }

    private void CreatePopup(PopupName popupName)
    {
        var popupView = popupDatabase[popupName];
        if (popupView != null)
        {
            var popupViewInstance = Instantiate(popupView, popupContainer);
            popupViewInstance.gameObject.SetActive(false);

            AbstractPopupController controller = _popupControllerFactory.CreateController(popupViewInstance, _uiNavigator);

            _popups.Add(popupName, popupViewInstance);
            _popupControllers.Add(popupViewInstance, controller);
        }
    }

    private void CreateBottomSheet(BottomSheetName sheetName)
    {
        var sheetView = bottomSheetDatabase[sheetName];
        if (sheetView != null)
        {
            var sheetViewInstance = Instantiate(sheetView, bottomSheetContainer);
            sheetViewInstance.gameObject.SetActive(false);

            AbstractBottomSheetController controller = _bottomSheetControllerFactory.CreateController(sheetViewInstance, _uiNavigator);

            _sheets.Add(sheetName, sheetViewInstance);
            _sheetControllers.Add(sheetViewInstance, controller);
        }
    }
}
