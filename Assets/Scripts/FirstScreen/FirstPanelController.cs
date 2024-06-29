using DefaultNamespace;

public class FirstPanelController : AbstractPanelController
{
    private First _firstPanel;
    private readonly ScreenNavigationSystem _navigationSystem;

    public FirstPanelController(First panelMono, ScreenNavigationSystem navigationSystem) 
        : base(panelMono, navigationSystem)
    {
        _firstPanel = panelMono;
        _navigationSystem = navigationSystem;
        _firstPanel.Button.onClick.AddListener(HandleButtonClick);
    }

    public void HandleButtonClick()
    {
        _navigationSystem.ShowScreen(PanelType.Second);
    }

    // Пам'ятайте очистити слухачі, коли контролер не потрібен
    public override void Dispose()
    {
        _firstPanel.Button.onClick.RemoveListener(HandleButtonClick);
    }
}