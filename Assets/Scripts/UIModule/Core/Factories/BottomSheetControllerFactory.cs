using System;
using ScreensRoot;

public class BottomSheetControllerFactory
{
    public AbstractBottomSheetController CreateController(AbstractBottomSheetView bottomSheetView,
        UINavigator uiNavigator)
    {
        return bottomSheetView switch
        {
            _ => new GenericBottomSheetController(bottomSheetView, uiNavigator)
        };
    }
}

public class GenericBottomSheetController : AbstractBottomSheetController
{
    public GenericBottomSheetController(AbstractBottomSheetView bottomSheetView, UINavigator uiNavigator)
        : base(bottomSheetView, uiNavigator)
    {
    }
}