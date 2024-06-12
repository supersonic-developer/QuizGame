using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Helpers;
using QuizGame.Models;
using QuizGame.Services.Interfaces;
using QuizGame.Views;

namespace QuizGame.ViewModels
{
    public partial class QuestionPageViewModel(Quiz quiz, Topics topics, HighlightJs highlightJs, HeaderViewModel headerViewModel, 
        IDialogService dialogService, ReferenceViewModel referenceViewModel) : ObservableObject
    {
        // Bindable properties
        [ObservableProperty]
        Question? displayedQuestion;

        // Models and services
        readonly HighlightJs highlightJs = highlightJs;
        readonly Quiz quiz = quiz;
        readonly Topics topics = topics;
        readonly HeaderViewModel headerViewModel = headerViewModel;
        readonly IDialogService dialogService = dialogService;

        // View models
        [ObservableProperty]
        CodeSnippetViewModel? codeSnippetViewModel;

        [ObservableProperty]
        ReferenceViewModel referenceViewModel = referenceViewModel;
        public List<AnswerViewModel> AnswerViewModels { get; } = [];

        // State variable, becomes true if the right answer was displayed
        bool isDisplayed = false;

        // Methods
        void SetHeader()
        {
            if (!(headerViewModel.Title == topics.TopicsData![(int)topics.SelectedTopicIdx!].Name))
            {
                headerViewModel.Title = topics.TopicsData![(int)topics.SelectedTopicIdx!].Name;
                headerViewModel.Subtitle = "";
                headerViewModel.ImagePath = "";
                headerViewModel.HomeImagePath = "home_icon.png";
            }
        }

        Question RandomElementAndRemove()
        {
            Random random = new();
            Question randQuestion = quiz.Questions![random.Next(quiz.Questions.Count)];
            quiz.Questions.Remove(randQuestion);
            return randQuestion;
        }


        // Commands
        [RelayCommand]
        void Appearing()
        {
            DisplayedQuestion = RandomElementAndRemove();
            CodeSnippetViewModel = new(DisplayedQuestion.CodeBlock, highlightJs);
            foreach (Answer answer in DisplayedQuestion.Answers)
            {
                AnswerViewModels.Add(new AnswerViewModel(answer, new CodeSnippetViewModel(answer.CodeBlock, highlightJs)));
            }
            SetHeader();
        }

        [RelayCommand]
        async Task NextAsync(Button button)
        {
            if (isDisplayed)
            {
                await Shell.Current.GoToAsync($"{nameof(QuestionPage)}");
            }
            else
            {
                isDisplayed = true;
                // Change the text of button
                button.Text = "Next";
                // Copy correctness into binded property
                
                AnswerViewModel? selected = AnswerViewModels.Find(vm => vm.AnswerState == AnswerViewModel.State.IsSelected);
                string title = selected?.Answer.IsCorrect == true ? "Correct Answer" : "Wrong Answer";
                string message = DisplayedQuestion?.Reference == null ? "Unfortunatelly, does not exist any reference or explanation for this question.\nDo you stay and explore more or proceed to next question?" :
                    "Luckily, some reference or explanation could be found for this question.\nDo you want to check it out or proceed to next question?";
                foreach (AnswerViewModel answerViewModel in AnswerViewModels)
                {
                    answerViewModel.AnswerState = answerViewModel.Answer.IsCorrect ? AnswerViewModel.State.CorrectDisplayed : AnswerViewModel.State.InCorrectDisplayed;
                }
                bool answer = await dialogService.DisplayAlertAsync(title, message, "Stay", "Proceed");
                if (!answer)
                {
                    await Shell.Current.GoToAsync($"{nameof(QuestionPage)}");
                }
            }
        }
    }
}
