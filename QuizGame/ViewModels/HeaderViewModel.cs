using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Models;
using QuizGame.Views;

namespace QuizGame.ViewModels
{
    public partial class HeaderViewModel(Quiz quiz) : ObservableObject
    {
        [ObservableProperty]
        string title = "";

        [ObservableProperty]
        string subtitle = "";

        [ObservableProperty]
        string imagePath = "";

        [ObservableProperty]
        string homeImagePath = "";

        readonly Quiz quiz = quiz;

        [RelayCommand]
        async Task ButtonClickedAsync()
        {
            quiz.Questions = null;
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}
