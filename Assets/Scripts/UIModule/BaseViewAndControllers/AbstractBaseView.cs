using System;
using UnityEngine;

namespace UIModule.BaseViewAndControllers
{
    public abstract class AbstractBaseView : MonoBehaviour
    {
        public event Action ViewEnabled;
        public event Action ViewDisabled;

        private void OnEnable() => ViewEnabled?.Invoke();
        private void OnDisable() => ViewDisabled?.Invoke();


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