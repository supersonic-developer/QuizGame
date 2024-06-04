using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Helpers;
using QuizGame.Models;
using System.Collections.ObjectModel;

namespace QuizGame.ViewModels
{
    public partial class QuestionViewModel : ObservableObject
    {
        // Bindable properties
        [ObservableProperty]
        Question displayedQuestion;

        [ObservableProperty]
        CodeSnippetViewModel codeViewModel;

        [ObservableProperty]
        bool isLoading = false;

        public ObservableCollection<AnswerViewModel> AnswerViewModels { get; }

        bool isAnswerShown;

        void SetHeader(HeaderViewModel headerViewModel, Topics topics)
        {
            headerViewModel.Title = topics.TopicsData[(int)topics.SelectedTopicIdx!].Name;
            headerViewModel.Subtitle = "";
            headerViewModel.ImagePath = "";
            headerViewModel.HomeImagePath = "home_icon.png";
        }


        // Constructor
        public QuestionViewModel(Topics topics, List<Question> questions, HighlightJs highlightJs, HeaderViewModel headerViewModel)
        {
            // Set header view model
            SetHeader(headerViewModel, topics);

            // Set view model properties
            DisplayedQuestion = RandomElementAndRemove(questions);
            CodeViewModel = new CodeSnippetViewModel(displayedQuestion.CodeBlock, highlightJs);
            AnswerViewModels = [];
            foreach (Answer answer in displayedQuestion.Answers)
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
            IsLoading = true;
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
            IsLoading = false;
        }
    }
}
