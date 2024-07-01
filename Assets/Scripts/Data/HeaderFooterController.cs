using System.Reflection;
using ScreensRoot;
using UnityEngine;

namespace Data
{
    public class HeaderFooterController
    {
        private readonly ScreenConfiguration _configuration;

        public HeaderFooterController(ScreenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ApplyHeaderSettings(AbstractScreenView screenView, ScreenNavigationSystem navigationSystem)
        {
            if (_configuration.headerSettings == null || _configuration.headerSettings.Count == 0)
            {
                screenView.HeaderView.gameObject.SetActive(false);
                return;
            }

            screenView.HeaderView.gameObject.SetActive(true);
            DisableUnusedElements(screenView.HeaderView);

            foreach (var setting in _configuration.headerSettings)
            {
                setting.ApplySetting(screenView, navigationSystem);
            }
        }

        public void ApplyFooterSettings(AbstractScreenView screenView)
        {
            if (_configuration.footerSettings == null || _configuration.footerSettings.Count == 0)
            {
                screenView.FooterView.gameObject.SetActive(false);
                return;
            }

            screenView.FooterView.gameObject.SetActive(true);
            DisableUnusedElements(screenView.FooterView);

            foreach (var setting in _configuration.footerSettings)
            {
                setting.ApplySetting(screenView, null);
            }
        }

        private void DisableUnusedElements(object view)
        {
            var viewGameObject = (view as Component)?.gameObject ?? view as GameObject;
            if (viewGameObject == null) return;

            var fields = view.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var properties = view.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(GameObject) || field.FieldType.IsSubclassOf(typeof(Component)))
                {
                    var value = field.GetValue(view) as GameObject ?? (field.GetValue(view) as Component)?.gameObject;
                    if (value != null && value != viewGameObject)
                    {
                        value.SetActive(false);
                    }
                }
            }

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(GameObject) ||
                    property.PropertyType.IsSubclassOf(typeof(Component)))
                {
                    var value = property.GetValue(view) as GameObject ??
                                (property.GetValue(view) as Component)?.gameObject;
                    if (value != null && value != viewGameObject)
                    {
                        value.SetActive(false);
                    }
                }
            }
        }
    }
}