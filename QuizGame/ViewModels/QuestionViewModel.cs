using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Helpers;
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

        public ObservableCollection<AnswerViewModel> AnswerViewModels { get; }

        public QuestionViewModel(List<Question> questions, HighlightJs highlightJs)
        {
            // Set view model properties
            Question = RandomElementAndRemove(questions);
            CodeViewModel = new CodeSnippetViewModel(question.CodeBlock, highlightJs);
            AnswerViewModels = [];
            foreach (Answer answer in Question.Answers)
            {
                AnswerViewModels.Add(new AnswerViewModel(answer, new CodeSnippetViewModel(answer.CodeBlock, highlightJs)));
            }
        }

        static Question RandomElementAndRemove(List<Question> questions)
        {
            //Random random = new();
            Question randQuestion = questions[1];
            questions.Remove(randQuestion);
            return randQuestion;
        }

        [RelayCommand]
        void Next(Button button)
        {
            button.Text = "Next";
            foreach (AnswerViewModel answerViewModel in AnswerViewModels)
            {
                answerViewModel.IsDisplayed = true;
            }
        }
    }
}
