using DefaultNamespace;

public class SecondPanelController : AbstractPanelController
{
    private Second _firstPanel;
    private readonly ScreenNavigationSystem _navigationSystem;

    public SecondPanelController(Second panelMono, ScreenNavigationSystem navigationSystem) 
        : base(panelMono, navigationSystem)
    {
        _firstPanel = panelMono;
        _navigationSystem = navigationSystem;
        _firstPanel.Button.onClick.AddListener(HandleButtonClick);
    }

    public void HandleButtonClick()
    {
        _navigationSystem.ShowScreen(PanelType.First);
    }

    // Пам'ятайте очистити слухачі, коли контролер не потрібен
    public override void Dispose()
    {
        _firstPanel.Button.onClick.RemoveListener(HandleButtonClick);
    }
}