using Microsoft.Maui.Handlers;

namespace QuizGame.Handlers;

public partial class SearchBarExHandler : SearchBarHandler
{
    public static readonly IPropertyMapper<ISearchBar, SearchBarHandler> SearchBarMapper =
        new PropertyMapper<ISearchBar, SearchBarHandler>(Mapper)
        {
            ["TextColor"] = MapIconColor,
        };

    public SearchBarExHandler() : base(SearchBarMapper, CommandMapper)
    {
    }

    public override void UpdateValue(string propertyName)
    {
        base.UpdateValue(propertyName);

        if (propertyName == SearchBar.TextColorProperty.PropertyName)
        {
            SetIconColor(GetTextColor());
        }
    }
}
