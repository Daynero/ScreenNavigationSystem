using System;
using DG.Tweening;
using UIModule.BaseViewAndControllers;
using UnityEngine;

namespace UIModule.Animations
{
    public class PopupAnimationController
    {
        private const float AnimationDuration = 0.5f;

        public void PlayAnimation(AbstractPopupView popupView, PopupTransitionType transitionType, Action onComplete)
        {
            SetPopupInteractable(popupView, false);
            popupView.gameObject.SetActive(true);
            var rectTransform = popupView.GetComponent<RectTransform>();

            switch (transitionType)
            {
                case PopupTransitionType.Fade:
                    var canvasGroup = rectTransform.GetComponent<CanvasGroup>();
                    canvasGroup.alpha = 0f;
                    canvasGroup.DOFade(1f, AnimationDuration).OnComplete(() =>
                    {
                        onComplete?.Invoke();
                        SetPopupInteractable(popupView, true);
                    });
                    break;

                case PopupTransitionType.Scale:
                    rectTransform.localScale = Vector3.zero;
                    rectTransform.DOScale(Vector3.one, AnimationDuration).OnComplete(() =>
                    {
                        onComplete?.Invoke();
                        SetPopupInteractable(popupView, true);
                    });
                    break;

                case PopupTransitionType.None:
                    onComplete?.Invoke();
                    break;
            }
        }

        private void SetPopupInteractable(AbstractPopupView popupView, bool interactable)
        {
            var canvasGroup = popupView.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.interactable = interactable;
            }
        }
    }
}