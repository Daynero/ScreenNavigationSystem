using UIModule.Animations;
using UIModule.BaseViewAndControllers;
using UIModule.BottomSheets;
using UIModule.Core;
using UIModule.Popups;
using UIModule.Screens;

namespace UIModule.NavigationSystems
{
    public interface IUINavigator
    {
        IUINavigator Show(ScreenName screenName, ScreenTransitionType transitionType = ScreenTransitionType.None);
        IUINavigator Show(PopupName popupName, PopupTransitionType transitionType = PopupTransitionType.None);
        IUINavigator Show(BottomSheetName bottomSheetName);
        IUINavigator With<T>(T data) where T : BaseVm;
        void CloseAll(UIType type = UIType.All);
    }
}