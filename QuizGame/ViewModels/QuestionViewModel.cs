using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Models;
using System.Collections.ObjectModel;

namespace QuizGame.ViewModels
{
    public partial class QuestionViewModel : ObservableObject
    {
        [ObservableProperty]
        Question question;

        [ObservableProperty]
        CodeSnippetViewModel codeViewModel;

        [ObservableProperty]
        ObservableCollection<AnswerViewModel> answerViewModels;

        public QuestionViewModel(Question question)
        {
            Question = question;
            CodeViewModel = new CodeSnippetViewModel(question.CodeBlock);
            AnswerViewModels = new ObservableCollection<AnswerViewModel>();
            foreach (Answer answer in Question.Answers)
            {
                AnswerViewModels.Add(new AnswerViewModel(answer));
            }
        }

        [RelayCommand]
        void CheckedChanged(CollectionView collectionView)
        {
            int stop = 1;
        }
    }
}
