using QuizGame.Services.Interfaces;
using QuizGame.Views;

namespace QuizGame.Services.Implementations
{
    public class DialogService : IDialogService
    {
        public async Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons) => 
            await Shell.Current.DisplayActionSheet(title, cancel, destruction, buttons);

        public async Task<bool> DisplayAlertAsync(string title, string text, string accept, string cancel) =>
            await Shell.Current.DisplayAlert(title, text, accept, cancel);
    }
}
