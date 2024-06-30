using System;
using System.Collections.Generic;
using DefaultNamespace;
using PanelsNavigationModule;
using UnityEngine;

public class CompositionRoot : MonoBehaviour
{
    [Header("Root Objects")]
    [SerializeField] private Transform allScreensContainer;
    [SerializeField] private PanelDatabase panelDatabase;
    [SerializeField] private PanelType defaultPanelType;

    private Dictionary<PanelType, AbstractPanelMono> _panels = new Dictionary<PanelType, AbstractPanelMono>();
    private Dictionary<AbstractPanelMono, AbstractPanelController> _controllers = new Dictionary<AbstractPanelMono, AbstractPanelController>();
    private ScreenNavigationSystem _screenNavigationSystem;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _screenNavigationSystem = new ScreenNavigationSystem(_panels, defaultPanelType);
        
        foreach (PanelType panelType in Enum.GetValues(typeof(PanelType)))
        {
            var panelPrefab = panelDatabase[panelType];
            if (panelPrefab != null)
            {
                var panelInstance = Instantiate(panelPrefab, allScreensContainer);
                panelInstance.gameObject.SetActive(false);

                AbstractPanelController controller;
                switch (panelType)
                {
                    case PanelType.First:
                        controller = new FirstPanelController(panelInstance as First, _screenNavigationSystem);
                        break;
                    case PanelType.Second:
                        controller = new SecondPanelController(panelInstance as Second, _screenNavigationSystem);
                        break;
                    default:
                        controller = new AbstractPanelController(panelInstance, _screenNavigationSystem);
                        break;
                }

                _panels.Add(panelType, panelInstance);
                _controllers.Add(panelInstance, controller);
            }
        }

        _screenNavigationSystem.ShowScreen(defaultPanelType);
    }
}
