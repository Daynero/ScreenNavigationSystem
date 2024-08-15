using DefaultNamespace;
using ScreensRoot;

public class BottomSheetNavigationSystem
{
    // Існуючий код...
    
    public void ShowWithData<T>(BottomSheetName bottomSheetName, T data) where T : BaseVm
    {
        // Логіка для показу нижнього листа з даними
    }

    public void CloseAll()
    {
        // Логіка для закриття всіх нижніх листів
    }

    public AbstractBottomSheetView Show(BottomSheetName bottomSheetName)
    {
        throw new System.NotImplementedException();
    }
}