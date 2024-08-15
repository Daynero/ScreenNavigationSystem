using System.Collections.Generic;
using System.Linq;
using CommonPanels;
using ScreensRoot;
using UnityEngine;

public class AbstractScreenController : AbstractBaseController
{
    private readonly AbstractScreenView ScreenView;
    private List<IResettable> _resettableComponents;
    protected Canvas Canvas;

    public AbstractScreenController(AbstractScreenView screenView, IUINavigator uiNavigator) : base(uiNavigator)
    {
        ScreenView = screenView;

        ScreenView.ViewEnabled += () => OnEnableHandler(_resettableComponents);
        ScreenView.ViewDisabled += () => OnDisableHandler(_resettableComponents);
        
        InitializeResettableComponents();
    }

    private void InitializeResettableComponents()
    {
        _resettableComponents = ScreenView.GetComponentsInChildren<MonoBehaviour>()
            .OfType<IResettable>()
            .ToList();
    }

    protected override Component GetMainComponent()
    {
        return ScreenView;
    }

    protected override void OnShow()
    {
        base.OnShow();
        SettingCanvas();
    }

    protected override void EnableInteraction()
    {
        ScreenView.EnableInteraction();
    }

    protected virtual void SettingCanvas()
    {
        if (Canvas == null)
        {
            Canvas = ScreenView.GetComponentInParent<Canvas>();
        }
        Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
    }
}