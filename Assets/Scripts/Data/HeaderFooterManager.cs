using ScreensRoot;

namespace Data
{
    public class HeaderFooterManager
    {
        private readonly ScreenConfiguration _configuration;

        public HeaderFooterManager(ScreenConfiguration configuration)
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
            DisableUnusedHeaderElements(screenView);

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
            DisableUnusedFooterElements(screenView);

            foreach (var setting in _configuration.footerSettings)
            {
                setting.ApplySetting(screenView, null);
            }
        }

        private void DisableUnusedHeaderElements(AbstractScreenView screenView)
        {
            screenView.HeaderView.BackButton?.gameObject.SetActive(false);
            screenView.HeaderView.Title?.gameObject.SetActive(false);
        }

        private void DisableUnusedFooterElements(AbstractScreenView screenView)
        {
        
        }
    }
}