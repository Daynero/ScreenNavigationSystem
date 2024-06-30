using System;

public class AbstractScreenController : IDisposable
{
    public AbstractScreenView ScreenViewView { get; private set; }
    protected ScreenNavigationSystem ScreenNavigationSystem { get; private set; }

    public event Action<object> OnShow;

    public AbstractScreenController(AbstractScreenView screenViewView, ScreenNavigationSystem screenNavigationSystem)
    {
        ScreenViewView = screenViewView;
        ScreenNavigationSystem = screenNavigationSystem;
    }

    public void Show()
    {
        ScreenViewView.gameObject.SetActive(true);
        OnShow?.Invoke(null);
    }

    public void ShowWithData(object data)
    {
        ScreenViewView.gameObject.SetActive(true);
        OnShow?.Invoke(data);
    }

    public void Hide()
    {
        ScreenViewView.gameObject.SetActive(false);
    }

    public virtual void Dispose()
    {
    }
}