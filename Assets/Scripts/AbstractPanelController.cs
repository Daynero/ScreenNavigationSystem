using System;

public class AbstractPanelController : IDisposable
{
    public AbstractPanelMono PanelMono { get; private set; }
    protected ScreenNavigationSystem NavigationSystem;

    public AbstractPanelController(AbstractPanelMono panelMono, ScreenNavigationSystem navigationSystem)
    {
        PanelMono = panelMono;
        NavigationSystem = navigationSystem;
    }

    public virtual void Dispose()
    {
        // Очистка ресурсів, якщо потрібно
    }

    // Можна додати додаткові методи для управління панеллю, якщо потрібно
}