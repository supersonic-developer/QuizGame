using CommunityToolkit.Mvvm.ComponentModel;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public partial class AnswerViewModel : ObservableObject
    {
        [ObservableProperty]
        Answer answer;

        [ObservableProperty]
        CodeSnippetViewModel codeSnippetViewModel;

        public AnswerViewModel(Answer answer) 
        {
            Answer = answer;
            codeSnippetViewModel = new CodeSnippetViewModel(answer.CodeBlock);
        }
    }
}
