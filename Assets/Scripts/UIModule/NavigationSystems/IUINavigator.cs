using System;
using Animations;
using DefaultNamespace;
using ScreensRoot;

public interface IUINavigator
{
    IUINavigator Show(ScreenName screenName, ScreenTransitionType transitionType = ScreenTransitionType.None);
    IUINavigator Show(PopupName popupName, PopupTransitionType transitionType = PopupTransitionType.None);
    IUINavigator Show(BottomSheetName bottomSheetName);
    IUINavigator With<T>(T data) where T : BaseVm;
    void CloseAll(UIType type = UIType.All);
}