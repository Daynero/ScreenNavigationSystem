using Animations;
using FirstScreen;
using ScreensRoot;

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
        }

        protected override void HandleData<T>(T data)
        {
            if (data is not FirstScreenVM vm) return;
            
            _view.Text.text = vm.InputString;
            _registrationStateManager.SaveData(ScreenName.Second, data);
        }

        private void HandleButtonClick()
        {
            _registrationStateManager.ClearData();
            ScreenNavigationSystem.Show(ScreenName.First, ScreenTransitionDirection.LeftToRight);
        }

        public override void Dispose()
        {
            _view.Button.onClick.RemoveListener(HandleButtonClick);
        }
    }
}