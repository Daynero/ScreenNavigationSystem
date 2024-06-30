using System.Collections.Generic;
using PanelsNavigationModule.Animations;
using UnityEngine;

namespace PanelsNavigationModule
{
    public class ScreenNavigationSystem
    {
        private Dictionary<PanelType, AbstractPanelMono> _panels;
        private PanelType _currentPanelType;

        public ScreenNavigationSystem(Dictionary<PanelType, AbstractPanelMono> panels, PanelType initialPanelType)
        {
            _panels = panels;
            _currentPanelType = initialPanelType;
        }

        public void ShowScreen(PanelType panelType, PanelTransitionDirection transitionDirection = PanelTransitionDirection.None)
        {
            if (!_panels.ContainsKey(panelType))
            {
                Debug.LogError($"Panel type {panelType} not found in panels.");
                return;
            }

            AbstractPanelMono currentPanel = _panels[_currentPanelType];
            AbstractPanelMono nextPanel = _panels[panelType];

            if (transitionDirection != PanelTransitionDirection.None)
            {
                var animationController = new ScreenAnimationController(currentPanel, nextPanel, transitionDirection);
                animationController.PlayAnimation();
            }
            else
            {
                currentPanel.gameObject.SetActive(false);
                nextPanel.gameObject.SetActive(true);
            }

            _currentPanelType = panelType;
        }
    }
}