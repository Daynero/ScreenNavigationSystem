using System;
using UnityEngine;

namespace ScreensRoot
{
    [RequireComponent(typeof(CanvasGroup))] 
    public abstract class AbstractScreenView : AbstractBaseView
    {
        public virtual ScreenName ScreenName { get; }
    }
}