
namespace SecondPopup
{
    public class SecondPopupController : AbstractPopupController
    {
        private readonly SecondPopupView _screenView;

        public SecondPopupController(SecondPopupView screenView, IUINavigator uiNavigator) :
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