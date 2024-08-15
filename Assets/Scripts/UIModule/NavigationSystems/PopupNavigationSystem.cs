using System;
using System.Collections.Generic;
using UIModule.Animations;
using UIModule.BaseViewAndControllers;
using UIModule.Popups;

namespace UIModule.NavigationSystems
{
    public class PopupNavigationSystem
    {
        private readonly Dictionary<PopupName, AbstractPopupView> _popups;
        private Dictionary<AbstractPopupView, AbstractPopupController> _controllers;
        private Queue<(PopupName popupName, BaseVm data, PopupTransitionType transitionType)> _popupQueue = new();
        private AbstractPopupView _currentPopup;
        private AbstractPopupController _currentController;
        private bool _isAnimating;
        private readonly PopupAnimationController _animationController;

        public event Action<PopupName> OnPopupMissing;

        public PopupNavigationSystem(Dictionary<PopupName, AbstractPopupView> popups)
        {
            _popups = popups;
            _animationController = new PopupAnimationController();
        }

        public void InitControllers(Dictionary<AbstractPopupView, AbstractPopupController> controllers) =>
            _controllers = controllers;

        public AbstractPopupView Show(PopupName popupName, PopupTransitionType transitionType = PopupTransitionType.None)
        {
            _popupQueue.Enqueue((popupName, null, transitionType));
            return TryShowNextPopup();
        }

        public AbstractPopupView ShowWithData<T>(PopupName popupName, T data, PopupTransitionType transitionType = PopupTransitionType.None) where T : BaseVm
        {
            _popupQueue.Enqueue((popupName, data, transitionType));
            return TryShowNextPopup();
        }

        private AbstractPopupView TryShowNextPopup()
        {
            if (_isAnimating || _currentPopup != null) return null;

            if (_popupQueue.Count == 0) return null;

            var (popupName, data, transitionType) = _popupQueue.Dequeue();

            if (!IsPopupAvailable(popupName)) return null;

            var nextPopup = _popups[popupName];
            _currentController = _controllers[nextPopup];
            _currentPopup = nextPopup;

            _animationController.PlayAnimation(nextPopup, transitionType, () =>
            {
                _currentController.ShowWithData(data);
                _isAnimating = false;
            });

            _currentController.OnPopupClosed += OnPopupClosed;
            return _currentPopup;
        }

        private void OnPopupClosed()
        {
            _currentPopup = null;
            _currentController.OnPopupClosed -= OnPopupClosed;
            TryShowNextPopup();
        }

        private bool IsPopupAvailable(PopupName popupName)
        {
            CheckAndTryCreatePopup(popupName);
        
            if (_popups.ContainsKey(popupName)) return true;
        
            return false;
        }

        private void CheckAndTryCreatePopup(PopupName popupName)
        {
            if (!_popups.ContainsKey(popupName))
            {
                OnPopupMissing?.Invoke(popupName);
            }
        }

        public void CloseAll()
        {
            if (_isAnimating || _currentPopup == null) return;

            if (_currentPopup != null)
            {
                _currentController.OnPopupClosed -= OnPopupClosed;
                _animationController.PlayAnimation(_currentPopup, PopupTransitionType.None, () =>
                {
                    _currentPopup = null;
                    _currentController = null;
                    TryShowNextPopup();
                });
            }

            while (_popupQueue.Count > 0)
            {
                var (popupName, data, transitionType) = _popupQueue.Dequeue();
                if (IsPopupAvailable(popupName))
                {
                    var popup = _popups[popupName];
                    var controller = _controllers[popup];

                    controller.OnPopupClosed -= OnPopupClosed;
                    _animationController.PlayAnimation(popup, PopupTransitionType.None, () =>
                    {
                        // Закриття виконується без додаткових дій, оскільки черга вже оброблена
                    });
                }
            }

            _popupQueue.Clear();
        }
    }
}
