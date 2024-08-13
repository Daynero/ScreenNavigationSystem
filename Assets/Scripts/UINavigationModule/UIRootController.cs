using Animations;
using DefaultNamespace;
using ScreensRoot;
using UINavigationModule;

public class UIRootController : IUIRootController
{
    private static UIRootController instance;

    private UIRootController() 
    {
        // Ініціалізація
    }

    public static UIRootController Instance => instance ??= new UIRootController();

    public void Show(ScreenName screenName, ScreenTransitionType transitionType = ScreenTransitionType.None)
    {
        throw new System.NotImplementedException();
    }

    public void Show(PopupName popupName, PopupTransitionType transitionType = PopupTransitionType.None)
    {
        throw new System.NotImplementedException();
    }

    public void Show(BottomSheetName bottomSheetName)
    {
        throw new System.NotImplementedException();
    }

    public void ShowWithData<T>(ScreenName screenName, T data, ScreenTransitionType transitionType = ScreenTransitionType.None) where T : BaseVm
    {
        throw new System.NotImplementedException();
    }

    public void ShowWithData<T>(PopupName popupName, T data, PopupTransitionType transitionType = PopupTransitionType.None) where T : BaseVm
    {
        throw new System.NotImplementedException();
    }

    public void ShowWithData<T>(BottomSheetName bottomSheetName, T data) where T : BaseVm
    {
        throw new System.NotImplementedException();
    }

    public void CloseAll(UIModalType modalType = UIModalType.All)
    {
        throw new System.NotImplementedException();
    }
}