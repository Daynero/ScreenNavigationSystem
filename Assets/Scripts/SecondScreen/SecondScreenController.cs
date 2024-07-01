using Animations;
using FirstScreen;
using ScreensRoot;
using UnityEngine;

namespace SecondScreen
{
    public class SecondScreenController : AbstractScreenController<FirstScreenVM>
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

        protected override void HandleTypedData(FirstScreenVM data)
        {
            _view.Text.text = data.InputString;
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