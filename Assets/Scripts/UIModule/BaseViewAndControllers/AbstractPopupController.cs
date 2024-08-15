using System;
using System.Collections.Generic;
using System.Linq;
using CommonPanels;
using ScreensRoot;
using UnityEngine;

public class AbstractPopupController : AbstractBaseController
{
    private readonly AbstractPopupView _popupView;
    private List<IResettable> _resettableComponents;

    public event Action OnPopupClosed; // Додано івент для повідомлення про закриття попапа

    public AbstractPopupController(AbstractPopupView popupView, IUINavigator uiNavigator) : base(uiNavigator)
    {
        _popupView = popupView;

        _popupView.ViewEnabled += () => OnEnableHandler(_resettableComponents);
        _popupView.ViewDisabled += () => OnDisableHandler(_resettableComponents);

        InitializeResettableComponents();
    }

    private void InitializeResettableComponents()
    {
        _resettableComponents = _popupView.GetComponentsInChildren<MonoBehaviour>()
            .OfType<IResettable>()
            .ToList();
    }

    protected override Component GetMainComponent()
    {
        return _popupView;
    }

    protected override void EnableInteraction()
    {
        _popupView.EnableInteraction();
    }

    protected void Close()
    {
        _popupView.gameObject.SetActive(false); // Деактивація попапа
        OnPopupClosed?.Invoke(); // Виклик івенту
    }
}