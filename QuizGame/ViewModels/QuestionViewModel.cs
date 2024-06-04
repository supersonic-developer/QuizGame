using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Helpers;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public partial class QuestionViewModel : ObservableObject
    {
        // Bindable properties
        [ObservableProperty]
        Question displayedQuestion;

        [ObservableProperty]
        CodeSnippetViewModel codeViewModel;

        public ObservableCollection<AnswerViewModel> AnswerViewModels { get; }

        bool isAnswerShown;


        // Constructor
        public QuestionViewModel(List<Question> questions, HighlightJs highlightJs)
        {
            // Set view model properties
            DisplayedQuestion = RandomElementAndRemove(questions);
            CodeViewModel = new CodeSnippetViewModel(question.CodeBlock, highlightJs);
            AnswerViewModels = [];
            foreach (Answer answer in Question.Answers)
            {
                AnswerViewModels.Add(new AnswerViewModel(answer, new CodeSnippetViewModel(answer.CodeBlock, highlightJs)));
            }
        }


        // Method to get a random element from a list and remove it
        static Question RandomElementAndRemove(List<Question> questions)
        {
            Random random = new();
            Question randQuestion = questions[random.Next(questions.Count)];
            questions.Remove(randQuestion);
            return randQuestion;
        }


        // Commands
        [RelayCommand]
        async Task Next(Button button)
        {
            if (isAnswerShown)
            {            
                await Shell.Current.GoToAsync($"{nameof(QuizPage)}");
            }
            else
            {
                isAnswerShown = true;
                // Change the text of button
                button.Text = "Next";
                // Copy correctness into binded property
                foreach (AnswerViewModel answerViewModel in AnswerViewModels)
                {
                    answerViewModel.IsDisplayed = answerViewModel.Answer.IsCorrect;
                }
            }
        }
    }
}
