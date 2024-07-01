using Data;
using UnityEngine;

namespace ScreensRoot
{
    [RequireComponent(typeof(CanvasGroup))] 
    public abstract class AbstractScreenView : MonoBehaviour
    {
        [field: SerializeField] public ScreenConfiguration ScreenConfiguration { get; private set; }
        public HeaderView HeaderView { get; private set; }
        public FooterView FooterView { get; private set; }
        public abstract ScreenName ScreenName { get; }

        public void Initialize(HeaderView headerViewPrefab, FooterView footerViewPrefab, Transform parent)
        {
            HeaderView = Instantiate(headerViewPrefab, parent);
            FooterView = Instantiate(footerViewPrefab, parent);
        }

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