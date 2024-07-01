using Animations;
using ScreensRoot;

namespace FirstScreen
{
    public class FirstScreenController : AbstractScreenController
    {
        private readonly FirstScreenView _view;

        public FirstScreenController(FirstScreenView screenViewView, ScreenNavigationSystem navigationSystem)
            : base(screenViewView, navigationSystem)
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

            ScreenNavigationSystem.ShowWithData(ScreenName.Second, firstScreenVm,
                ScreenTransitionDirection.RightToLeft);
        }

        public override void Dispose()
        {
            _view.Button.onClick.RemoveListener(HandleButtonClick);
        }
    }
}