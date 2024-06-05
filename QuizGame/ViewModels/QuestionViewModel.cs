using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuizGame.Helpers;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public partial class QuestionViewModel : ObservableObject
    {
        // Bindable properties
        public Question DisplayedQuestion { get; }

        public CodeSnippetViewModel CodeSnippetViewModel { get; }

        public List<AnswerViewModel> AnswerViewModels { get; }

        bool isDisplayed = false;


        // Constructor
        public QuestionViewModel(List<Question> questions, HighlightJs highlightJs)
        {
            // Select question randomly
            DisplayedQuestion = RandomElementAndRemove(questions);

            // Create view models
            CodeSnippetViewModel = new CodeSnippetViewModel(DisplayedQuestion.CodeBlock, highlightJs);
            AnswerViewModels = [];
            foreach (Answer answer in DisplayedQuestion.Answers)
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
        async Task NextAsync(Button button)
        {
            if (isDisplayed)
            {
                WeakReferenceMessenger.Default.Send(new NavigationRequestedMessage());
                await Shell.Current.GoToAsync($"{nameof(QuizPage)}");
                WeakReferenceMessenger.Default.Send(new NavigationCompletedMessage());
            }
            else
            {
                isDisplayed = true;
                // Change the text of button
                button.Text = "Next";
                // Copy correctness into binded property
                foreach (AnswerViewModel answerViewModel in AnswerViewModels)
                {
                    answerViewModel.AnswerState = answerViewModel.Answer.IsCorrect ? AnswerViewModel.State.CorrectDisplayed : AnswerViewModel.State.InCorrectDisplayed;
                }
            }
        }
    }
}
