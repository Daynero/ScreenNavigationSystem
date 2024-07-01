using System;
using FirstScreen;
using UnityEngine;

namespace ScreensRoot
{
    public abstract class AbstractScreenController : IDisposable
    {
        private AbstractScreenView ScreenView { get; set; }
        protected ScreenNavigationSystem ScreenNavigationSystem { get; private set; }

        protected AbstractScreenController(AbstractScreenView screenView, ScreenNavigationSystem screenNavigationSystem)
        {
            ScreenView = screenView;
            ScreenNavigationSystem = screenNavigationSystem;
        }

        public void Show()
        {
            ScreenView.gameObject.SetActive(true);
        }

        public void ShowWithData<T>(T data) where T : BaseVm
        {
            ScreenView.gameObject.SetActive(true);
            HandleData(data);
        }

        protected virtual void HandleData<T>(T data) where T : BaseVm
        {
            
        }

        public void Hide()
        {
            ScreenView.gameObject.SetActive(false);
        }

        public virtual void Dispose()
        {
        
        }
    }


    public abstract class AbstractScreenController<T> : AbstractScreenController where T : BaseVm
    {
        protected AbstractScreenController(AbstractScreenView screenView, ScreenNavigationSystem screenNavigationSystem)
            : base(screenView, screenNavigationSystem)
        {
        }

        protected override void HandleData<TData>(TData data)
        {
            if (data is T typedData)
            {
                HandleTypedData(typedData);
            }
            else
            {
                Debug.LogError($"Неправильний тип даних: {typeof(TData).FullName}, очікується: {typeof(T).FullName}");
            }
        }

        protected abstract void HandleTypedData(T data);
    }
}