using Animations;
using DefaultNamespace;
using ScreensRoot;

namespace UINavigationModule
{
    public interface IUIRootController
    {
        public void Show(ScreenName screenName,
            ScreenTransitionType transitionType = ScreenTransitionType.None);
        
        public void Show(PopupName popupName,
            PopupTransitionType transitionType = PopupTransitionType.None);

        public void Show(BottomSheetName bottomSheetName);
        
        public void ShowWithData<T>(ScreenName screenName, T data,
            ScreenTransitionType transitionType = ScreenTransitionType.None) where T : BaseVm;

        public void ShowWithData<T>(PopupName popupName, T data,
            PopupTransitionType transitionType = PopupTransitionType.None) where T : BaseVm;

        public void ShowWithData<T>(BottomSheetName bottomSheetName, T data) where T : BaseVm;

        public void CloseAll(UIModalType modalType = UIModalType.All);
    }
}