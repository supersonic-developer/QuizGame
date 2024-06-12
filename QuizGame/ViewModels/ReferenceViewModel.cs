using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace QuizGame.ViewModels
{
    public partial class ReferenceViewModel : ObservableObject
    {
        [RelayCommand]
        async Task TapAsync(string url) => await Browser.OpenAsync(url);
    }
}
