using Animations;
using FirstScreen;

namespace SecondScreen
{
    public class SecondScreenController : AbstractScreenController
    {
        private readonly SecondScreenView _view;
        private readonly RegistrationStateManager _registrationStateManager;

        public SecondScreenController(SecondScreenView view, ScreenNavigationSystem navigationSystem,
            RegistrationStateManager registrationStateManager)
            : base(view, navigationSystem)
        {
            _view = view;
            _registrationStateManager = registrationStateManager;
            _view.Button.onClick.AddListener(HandleButtonClick);

            OnShow += HandleData;
        }

        private void HandleData(object data)
        {
            FirstScreenVM vm = data as FirstScreenVM;
            _view.Text.text = vm.InputString;
            _registrationStateManager.SaveData(ScreenName.Second, vm);
        }

        private void HandleButtonClick()
        {
            ScreenNavigationSystem.Show(ScreenName.First, ScreenTransitionDirection.LeftToRight);
        }

        public override void Dispose()
        {
            _view.Button.onClick.RemoveListener(HandleButtonClick);
        }
    }
}