using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Models;
using QuizGame.Helpers;
using QuizGame.Views;
using QuizGame.Services.Interfaces;
using QuizGame.Services.Implementations;
using System.Collections.ObjectModel;

namespace QuizGame.ViewModels
{
    public partial class QuestionPageViewModel : ObservableObject
    {
        // Bindable properties
        [ObservableProperty]
        Question displayedQuestion;

        public CodeSnippetViewModel CodeSnippetViewModel { get; }

        public List<AnswerViewModel> AnswerViewModels { get; } = [];

        bool isDisplayed = false;
        readonly HighlightJs highlightJs;
        readonly Quiz quiz;

        // Constructor
        public QuestionPageViewModel(Quiz quiz, Topics topics, HighlightJs highlightJs, HeaderViewModel headerVm)
        {
            // Set data
            this.quiz = quiz;
            this.highlightJs = highlightJs;
            SetHeader(headerVm, topics);
            DisplayedQuestion = RandomElementAndRemove();
            CodeSnippetViewModel = new(DisplayedQuestion.CodeBlock, highlightJs);
            foreach (Answer answer in DisplayedQuestion.Answers)
            {
                AnswerViewModels.Add(new AnswerViewModel(answer, new CodeSnippetViewModel(answer.CodeBlock, highlightJs)));
            }
        }


        // Methods
        static void SetHeader(HeaderViewModel headerViewModel, Topics topics)
        {
            headerViewModel.Title = topics.TopicsData[(int)topics.SelectedTopicIdx!].Name;
            headerViewModel.Subtitle = "";
            headerViewModel.ImagePath = "";
            headerViewModel.HomeImagePath = "home_icon.png";
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
                foreach (AnswerViewModel answerViewModel in AnswerViewModels)
                {
                    answerViewModel.AnswerState = answerViewModel.Answer.IsCorrect ? AnswerViewModel.State.CorrectDisplayed : AnswerViewModel.State.InCorrectDisplayed;
                }
            }
        }
    }
}
