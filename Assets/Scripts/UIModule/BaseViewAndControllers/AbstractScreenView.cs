using UIModule.Screens;
using UnityEngine;

namespace UIModule.BaseViewAndControllers
{
    [RequireComponent(typeof(CanvasGroup))] 
    public abstract class AbstractScreenView : AbstractBaseView
    {
        public virtual ScreenName ScreenName { get; }
    }
}