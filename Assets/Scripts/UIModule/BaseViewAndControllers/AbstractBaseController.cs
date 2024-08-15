using System;
using System.Collections.Generic;
using System.Linq;
using CommonPanels;
using Tools;
using UnityEngine;

namespace ScreensRoot
{
    public abstract class AbstractBaseController : IDisposable
    {
        protected IUINavigator UINavigator { get; private set; }
        protected EventSubscriptions EventSubscriptions { get; private set; }
        protected Canvas Canvas;
        private List<IResettable> _resettableComponents;
        protected bool isResetComponent = true;

        protected AbstractBaseController(IUINavigator uiNavigator)
        {
            UINavigator = uiNavigator;
            EventSubscriptions = new();
        }

        protected void ResetComponents(List<IResettable> resettableComponents)
        {
            if (!isResetComponent) return;

            foreach (var resettable in resettableComponents)
            {
                resettable.ResetToDefault();
            }
        }

        protected abstract Component GetMainComponent();

        protected void OnEnableHandler(List<IResettable> resettableComponents)
        {
            ResetComponents(resettableComponents);
            OnShow();
        }

        protected void OnDisableHandler(List<IResettable> resettableComponents)
        {
            ResetComponents(resettableComponents);
            OnHide();
        }

        public virtual void Dispose()
        {
            EventSubscriptions.ClearSubscriptions();
        }

        protected virtual void OnShow() { }

        protected virtual void OnHide() { }

        public void ShowWithData<T>(T data) where T : BaseVm
        {
            Show();
            HandleData(data);
        }

        public void Show()
        {
            GetMainComponent().gameObject.SetActive(true);
            EnableInteraction();
        }

        public void Hide()
        {
            GetMainComponent().gameObject.SetActive(false);
        }

        protected virtual void HandleData<T>(T data) where T : BaseVm { }

        protected virtual void EnableInteraction() { }
    }
}