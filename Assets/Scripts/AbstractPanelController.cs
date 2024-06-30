using System;
using PanelsNavigationModule;

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
    }
}