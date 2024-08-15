using System.Collections.Generic;
using System.Linq;
using CommonPanels;
using ScreensRoot;
using UnityEngine;

public class AbstractBottomSheetController : AbstractBaseController
{
    private readonly AbstractBottomSheetView _bottomSheetView;
    private List<IResettable> _resettableComponents;

    public AbstractBottomSheetController(AbstractBottomSheetView bottomSheetView, IUINavigator uiNavigator) :
        base(uiNavigator)
    {
        _bottomSheetView = bottomSheetView;

        _bottomSheetView.ViewEnabled += () => OnEnableHandler(_resettableComponents);
        _bottomSheetView.ViewDisabled += () => OnDisableHandler(_resettableComponents);

        InitializeResettableComponents();
    }

    private void InitializeResettableComponents()
    {
        _resettableComponents = _bottomSheetView.GetComponentsInChildren<MonoBehaviour>()
            .OfType<IResettable>()
            .ToList();
    }

    protected override Component GetMainComponent()
    {
        return _bottomSheetView;
    }

    protected override void EnableInteraction()
    {
        _bottomSheetView.EnableInteraction();
    }
}