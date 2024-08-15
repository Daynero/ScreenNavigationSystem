using FirstScreen;
using ScreensRoot;
using SecondScreen;

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