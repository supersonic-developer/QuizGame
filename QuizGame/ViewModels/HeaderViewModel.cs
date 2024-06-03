using CommunityToolkit.Mvvm.ComponentModel;

namespace QuizGame.ViewModels
{
    public partial class HeaderViewModel : ObservableObject
    {
        [ObservableProperty]
        string title = "";

        [ObservableProperty]
        string subtitle = "";

        [ObservableProperty]
        string imagePath = "";
    }
}
