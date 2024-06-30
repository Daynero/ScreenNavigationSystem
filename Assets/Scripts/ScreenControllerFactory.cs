using FirstScreen;
using SecondScreen;

public class ScreenControllerFactory
{
    public AbstractScreenController CreateController(AbstractScreenView screenView, ScreenNavigationSystem navigationSystem, RegistrationStateManager registrationStateManager = null)
    {
        switch (screenView)
        {
            case FirstScreenView firstScreenView:
                return new FirstScreenController(firstScreenView, navigationSystem);
            case SecondScreenView secondScreenView:
                return new SecondScreenController(secondScreenView, navigationSystem, registrationStateManager);
            default:
                return new AbstractScreenController(screenView, navigationSystem);
        }
    }
}