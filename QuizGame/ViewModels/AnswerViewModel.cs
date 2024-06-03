using CommunityToolkit.Mvvm.ComponentModel;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public partial class AnswerViewModel(Answer answer, CodeSnippetViewModel? codeSnippetViewModel = null) : ObservableObject
    {
        [ObservableProperty]
        Answer answer = answer;

        [ObservableProperty]
        CodeSnippetViewModel? codeSnippetViewModel = codeSnippetViewModel;

        [ObservableProperty]
        bool isDisplayed = false;
    }
}
