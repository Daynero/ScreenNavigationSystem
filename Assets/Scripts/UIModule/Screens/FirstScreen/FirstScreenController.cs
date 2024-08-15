using UIModule.Animations;
using UIModule.BaseViewAndControllers;
using UIModule.NavigationSystems;
using UIModule.Popups;

namespace UIModule.Screens.FirstScreen
{
    public class FirstScreenController : AbstractScreenController
    {
        private readonly FirstScreenView _view;

        public FirstScreenController(FirstScreenView screenViewView, UINavigator uiNavigator)
            : base(screenViewView, uiNavigator)
        {
            _view = screenViewView;
            _view.Button.onClick.AddListener(HandleButtonClick);
            _view.ButtonPopup.onClick.AddListener(HandleButtonPopupClick);
        }

        private void HandleButtonClick()
        {
            FirstScreenVM firstScreenVm = new FirstScreenVM()
            {
                InputString = _view.InputField.text
            };

            UINavigator.Show(ScreenName.Second, ScreenTransitionType.RightToLeft).With(firstScreenVm);
        }

        private void HandleButtonPopupClick()
        {
            UINavigator.Show(PopupName.First, PopupTransitionType.Fade);
            UINavigator.Show(PopupName.Second, PopupTransitionType.Scale);
        }

        public override void Dispose()
        {
            _view.Button.onClick.RemoveListener(HandleButtonClick);
        }
    }
}