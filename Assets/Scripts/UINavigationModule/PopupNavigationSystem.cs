using Animations;
using DefaultNamespace;
using ScreensRoot;

public class PopupNavigationSystem
{
    // Існуючий код...
    
    public void ShowWithData<T>(PopupName popupName, T data) where T : BaseVm
    {
        // Логіка для показу попапу з даними
    }

    public void CloseAll()
    {
        // Логіка для закриття всіх попапів
    }

    public AbstractPopupView Show(PopupName popupName, PopupTransitionType transitionType)
    {
        throw new System.NotImplementedException();
    }
}