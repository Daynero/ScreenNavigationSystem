using Animations;
using ScreensRoot;

namespace FirstScreen
{
    public class FirstScreenController : AbstractScreenController
    {
        private readonly FirstScreenView _view;

        public FirstScreenController(FirstScreenView screenViewView, UINavigator uiNavigator)
            : base(screenViewView, uiNavigator)
        {
            _view = screenViewView;
            _view.Button.onClick.AddListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            FirstScreenVM firstScreenVm = new FirstScreenVM()
            {
                InputString = _view.InputField.text
            };

            UINavigator.Show(ScreenName.Second, ScreenTransitionType.RightToLeft).WithData(firstScreenVm);
        }

        public override void Dispose()
        {
            _view.Button.onClick.RemoveListener(HandleButtonClick);
        }
    }
}