using System;
using System.Collections.Generic;

public class ScreenNavigationSystem
{
    private readonly Action<PanelType> _showScreen;
    private Dictionary<PanelType, AbstractPanelController> _controllers;

    public ScreenNavigationSystem(Action<PanelType> showScreen)
    {
        _showScreen = showScreen;
    }

    public void Initialize(Dictionary<PanelType, AbstractPanelController> controllers)
    {
        _controllers = controllers;
    }

    public void ShowScreen(PanelType panelType)
    {
        _showScreen(panelType);
    }
}