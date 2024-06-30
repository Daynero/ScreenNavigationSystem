using DefaultNamespace;
using PanelsNavigationModule;
using PanelsNavigationModule.Animations;

public class FirstPanelController : AbstractPanelController
{
    private readonly First _firstPanel;

    public FirstPanelController(First panelMono, ScreenNavigationSystem navigationSystem) 
        : base(panelMono, navigationSystem)
    {
        _firstPanel = panelMono;
        _firstPanel.Button.onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
        NavigationSystem.ShowScreen(PanelType.Second, PanelTransitionDirection.RightToLeft);
    }

    public override void Dispose()
    {
        _firstPanel.Button.onClick.RemoveListener(HandleButtonClick);
    }
}