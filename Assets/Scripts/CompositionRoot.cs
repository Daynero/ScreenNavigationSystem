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

    private Dictionary<PanelType, AbstractPanelController> _controllers = new();
    private ScreenNavigationSystem _screenNavigationSystem;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _screenNavigationSystem = new ScreenNavigationSystem(ShowScreen);

        // Створюємо контролери для всіх екранів
        foreach (PanelType panelType in Enum.GetValues(typeof(PanelType)))
        {
            var panelPrefab = panelDatabase[panelType];
            if (panelPrefab != null)
            {
                var panelInstance = Instantiate(panelPrefab, allScreensContainer);
                panelInstance.gameObject.SetActive(false); // Робимо панелі неактивними на старті

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
                _controllers.Add(panelType, controller);
            }
        }

        _screenNavigationSystem.Initialize(_controllers);

        // Активуємо дефолтний екран
        ShowScreen(defaultPanelType);
    }

    private void ShowScreen(PanelType panelType)
    {
        foreach (var controller in _controllers.Values)
        {
            controller.PanelMono.gameObject.SetActive(controller.PanelMono.PanelType == panelType);
        }
    }
}
