using Animations;
using FirstScreen;

namespace SecondScreen
{
    public class SecondScreenController : AbstractScreenController
    {
        private readonly SecondScreenView _view;

        public SecondScreenController(SecondScreenView view, ScreenNavigationSystem navigationSystem) 
            : base(view, navigationSystem)
        {
            _view = view;
            _view.Button.onClick.AddListener(HandleButtonClick);

            OnShow += HandleData;
        }

        private void HandleData(object data)
        {
            FirstScreenVM vm = data as FirstScreenVM;
            _view.Text.text = vm.InputString;
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