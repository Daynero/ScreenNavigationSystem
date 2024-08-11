using System;
using UnityEngine;

namespace ScreensRoot
{
    [RequireComponent(typeof(CanvasGroup))] 
    public abstract class AbstractScreenView : MonoBehaviour
    {
        public abstract ScreenName ScreenName { get; }
        public event Action ScreenEnabled;
        public event Action ScreenDisable;

        private void OnEnable() => ScreenEnabled?.Invoke();
        private void OnDisable() => ScreenDisable?.Invoke();


        private CanvasGroup _canvasGroup;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void DisableInteraction()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void EnableInteraction()
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}