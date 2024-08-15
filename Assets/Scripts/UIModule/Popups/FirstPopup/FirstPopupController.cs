
using UIModule.BaseViewAndControllers;
using UIModule.NavigationSystems;

namespace UIModule.Popups.FirstPopup
{
    public class FirstPopupController : AbstractPopupController
    {
        private readonly FirstPopupView _screenView;

        public FirstPopupController(FirstPopupView screenView, IUINavigator uiNavigator) :
            base(screenView, uiNavigator)
        {
            _screenView = screenView;

            Init();
        }

        private void Init()
        {
            EventSubscriptions.AddSubscription(_screenView.Button, ClosePopup);
        }

        private void ClosePopup()
        {
            Close();
        }
    }
}