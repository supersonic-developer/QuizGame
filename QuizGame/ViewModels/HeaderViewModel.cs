using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuizGame.Helpers;

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

        [ObservableProperty]
        string homeImagePath = "";

        [RelayCommand]
        async Task ButtonClickedAsync()
        {
            WeakReferenceMessenger.Default.Send(new NavigationRequestedMessage());
            WeakReferenceMessenger.Default.Send(new UpdateHeaderMessage());
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            WeakReferenceMessenger.Default.Send(new NavigationCompletedMessage());
        }
    }
}
