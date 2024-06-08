using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

namespace QuizGame.Handlers;

public partial class SearchBarExHandler : SearchBarHandler
{
    public void SetIconColor(UIColor value)
    {
#if IOS13_0_OR_GREATER
#pragma warning disable CA1416 // Validate platform compatibility
        var textField = PlatformView.SearchTextField;
#pragma warning restore CA1416 // Validate platform compatibility
        var leftView = textField.LeftView ?? throw new Exception();
        leftView.TintColor = value;
#endif
    }

    private UIColor GetTextColor() => VirtualView.TextColor.ToPlatform();

    public static void MapIconColor(ISearchBarHandler handler, ISearchBar searchBar)
    {
        if (handler is SearchBarExHandler customHandler)
            customHandler.SetIconColor(customHandler.GetTextColor());
    }
}
