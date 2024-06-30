using DefaultNamespace;
using PanelsNavigationModule;
using PanelsNavigationModule.Animations;

public class SecondPanelController : AbstractPanelController
{
    private readonly Second _firstPanel;

    public SecondPanelController(Second panelMono, ScreenNavigationSystem navigationSystem) 
        : base(panelMono, navigationSystem)
    {
        _firstPanel = panelMono;
        _firstPanel.Button.onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        NavigationSystem.ShowScreen(PanelType.First, PanelTransitionDirection.LeftToRight);
    }

    public override void Dispose()
    {
        _firstPanel.Button.onClick.RemoveListener(HandleButtonClick);
    }
}