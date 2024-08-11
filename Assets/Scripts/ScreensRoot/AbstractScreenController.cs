using System;
using System.Collections.Generic;
using System.Linq;
using CommonPanels;
using Tools;
using UnityEngine;

namespace ScreensRoot
{
    public abstract class AbstractScreenController : IDisposable
    {
        private AbstractScreenView ScreenView { get; set; }
        protected ScreenNavigationSystem ScreenNavigationSystem { get; private set; }
        protected EventSubscriptions EventSubscriptions { get; private set; }
        protected Canvas Canvas;
        private List<IResettable> _resettableComponents;
        protected bool isResetComponent = true;

        protected AbstractScreenController(AbstractScreenView screenView, ScreenNavigationSystem screenNavigationSystem)
        {
            ScreenView = screenView;
            ScreenNavigationSystem = screenNavigationSystem;
            EventSubscriptions = new();
            ScreenView.ScreenEnabled += ScreenEnabledHandler;
            ScreenView.ScreenDisable += ScreenDisabledHandler;
            
            InitializeResettableComponents();
        }

        private void InitializeResettableComponents()
        {
            _resettableComponents = ScreenView.GetComponentsInChildren<MonoBehaviour>()
                .OfType<IResettable>()
                .ToList();
        }

        private void ScreenEnabledHandler()
        {
            ResetComponents();
            OnShow();
        }

        private void ScreenDisabledHandler()
        {
            ResetComponents();
            OnHide();
        }

        protected virtual void SettingCanvas()
        {
            if (Canvas == null)
            {
                Canvas = ScreenView.GetComponentInParent<Canvas>();
            }
            Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        }

        public void Show()
        {
            ScreenView.gameObject.SetActive(true);
            ScreenView.EnableInteraction();
        }

        protected virtual void OnShow()
        {
            SettingCanvas();
        }
        
        public void Hide()
        {
            ScreenView.gameObject.SetActive(false);
        }
        
        protected virtual void OnHide() { }

        private void ResetComponents()
        {
            if(!isResetComponent) return;
            
            foreach (var resettable in _resettableComponents)
            {
                resettable.ResetToDefault();
            }
        }

        public void ShowWithData<T>(T data) where T : BaseVm
        {
            Show();
            HandleData(data);
        }

        protected virtual void HandleData<T>(T data) where T : BaseVm
        {
            // Handle specific data
        }

        public virtual void Dispose()
        {
            EventSubscriptions.ClearSubscriptions();
        }
    }
}
