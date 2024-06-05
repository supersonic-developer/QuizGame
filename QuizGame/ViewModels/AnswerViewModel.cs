using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public partial class AnswerViewModel(Answer answer, CodeSnippetViewModel codeSnippetViewModel) : ObservableObject
    {
        public Answer Answer { get; } = answer;

        public CodeSnippetViewModel CodeSnippetViewModel { get; } = codeSnippetViewModel;

        [ObservableProperty]
        State answerState = Application.Current?.RequestedTheme == AppTheme.Light ? State.LightTheme : State.DarkTheme;

        public enum State
        {
            LightTheme,
            DarkTheme,
            IsSelected,
            InCorrectDisplayed,
            CorrectDisplayed
        }

        [RelayCommand]
        void CheckedChanged(bool isChecked)
        {
            AnswerState = AnswerState switch
            {
                State.LightTheme => isChecked ? State.IsSelected : State.LightTheme,
                State.DarkTheme => isChecked ? State.IsSelected : State.DarkTheme,
                // If unchecked set current theme, else remain selected
                State.IsSelected => !isChecked ? (Application.Current?.RequestedTheme == AppTheme.Light ? State.LightTheme : State.DarkTheme) : State.IsSelected,
                _ => AnswerState,
            };
        }
    }
}
