using UIModule.BaseViewAndControllers;
using UIModule.NavigationSystems;
using UIModule.Popups.FirstPopup;
using UIModule.Popups.SecondPopup;

namespace UIModule.Core.Factories
{
    internal class PopupControllerFactory
    {
        public AbstractPopupController CreateController(AbstractPopupView popupView, UINavigator uiNavigator)
        {
            return popupView switch
            {
                FirstPopupView firstPopupView => new FirstPopupController(firstPopupView, uiNavigator),
                SecondPopupView secondPopupView => new SecondPopupController(secondPopupView, uiNavigator),
                _ => new GenericPopupController(popupView, uiNavigator)
            };
        }
    }

    internal class GenericPopupController : AbstractPopupController
    {
        public GenericPopupController(AbstractPopupView popupView, UINavigator uiNavigator)
            : base(popupView, uiNavigator)
        {
        }
    }
}