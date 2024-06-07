using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuizGame.Helpers;
using QuizGame.Views;
using QuizGame.Models;

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
