using UIModule.Animations;
using UIModule.BaseViewAndControllers;
using UIModule.NavigationSystems;
using UIModule.Screens.FirstScreen;

namespace UIModule.Screens.SecondScreen
{
    public class SecondScreenController : AbstractScreenController
    {
        private readonly SecondScreenView _view;

        public SecondScreenController(SecondScreenView view, IUINavigator uiNavigator)
            : base(view, uiNavigator)
        {
            _view = view;
            _view.Button.onClick.AddListener(HandleButtonClick);
        }

        protected override void HandleData<T>(T data)
        {
            if (data is not FirstScreenVM vm) return;
            
            _view.Text.text = vm.InputString;
            
        }

        private void HandleButtonClick()
        {
            UINavigator.Show(ScreenName.First, ScreenTransitionType.LeftToRight);
        }

        public override void Dispose()
        {
            _view.Button.onClick.RemoveListener(HandleButtonClick);
        }
    }
}