using UIModule.BaseViewAndControllers;
using UIModule.NavigationSystems;
using UIModule.Screens.FirstScreen;
using UIModule.Screens.SecondScreen;

namespace UIModule.Core.Factories
{
    public class ScreenControllerFactory
    {
        public AbstractScreenController CreateController(AbstractScreenView screenView,
            UINavigator uiNavigator)
        {
            return screenView switch
            {
                FirstScreenView firstScreenView => new FirstScreenController(firstScreenView, uiNavigator),
                SecondScreenView secondScreenView => new SecondScreenController(secondScreenView, uiNavigator),
                _ => new GenericScreenController(screenView, uiNavigator)
            };
        }
    }

    public class GenericScreenController : AbstractScreenController
    {
        public GenericScreenController(AbstractScreenView screenView, UINavigator uiNavigator)
            : base(screenView, uiNavigator)
        {
        }
    }
}