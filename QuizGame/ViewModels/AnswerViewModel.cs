using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public partial class AnswerViewModel : ObservableObject
    {
        public Answer Answer { get; }

        public CodeSnippetViewModel CodeSnippetViewModel { get; }

        [ObservableProperty]
        State answerState = Application.Current?.RequestedTheme == AppTheme.Light ? State.LightTheme : State.DarkTheme;

        public AnswerViewModel(Answer answer, CodeSnippetViewModel codeSnippetViewModel)
        {
            Answer = answer;
            CodeSnippetViewModel = codeSnippetViewModel;
            Application.Current!.RequestedThemeChanged += (_, _) => 
            {
                if (AnswerState == State.LightTheme || AnswerState == State.DarkTheme)
                    AnswerState = Application.Current?.RequestedTheme == AppTheme.Light ? State.LightTheme : State.DarkTheme;
            };
        }

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
