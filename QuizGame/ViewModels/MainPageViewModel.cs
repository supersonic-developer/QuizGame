using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuizGame.Helpers;
using QuizGame.Models;
using QuizGame.Services.Interfaces;
using QuizGame.Views;

namespace QuizGame.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        // Binded properties
        [ObservableProperty]
        bool isLoading = false;

        [ObservableProperty]
        List<string> selectedTopicNames = [];

        // Models and services
        readonly Topics topics;
        // View models
        public HeaderViewModel HeaderViewModel { get; }
        readonly HighlightJs highlightJs;
        readonly Quiz quiz;

        // Cancellation token source for asynchronous search
        CancellationTokenSource cts = new();


        // Constructor
        public MainPageViewModel(Topics topics, HeaderViewModel headerViewModel, HighlightJs highlightJs, Quiz quiz)
        {
            HeaderViewModel = headerViewModel;
            SetHeader();

            this.topics = topics;
            this.highlightJs = highlightJs;
            this.quiz = quiz;

            // Register messages
            WeakReferenceMessenger.Default.Register<OnNavigationMessage>(this, (_, _) => SetHeader());
        }


        // Methods
        void SetHeader()
        {
            HeaderViewModel.Title = "Linked";
            HeaderViewModel.Subtitle = " Quizzes";
            HeaderViewModel.ImagePath = "linkedin_logo.png";
            HeaderViewModel.HomeImagePath = "";
        }

        List<string> GetNames() => topics.TopicsData.Select(element => element.Name).ToList();

        void SetNames() => SelectedTopicNames = topics.TopicsData.Select(element => element.Name).ToList();


        // Commands
        [RelayCommand]
        async Task AppearingAsync()
        { 
            IsLoading = true;
            await topics.InitAsync();
            SetNames();
            IsLoading = false;
        }

        [RelayCommand]
        static void Disappearing(SearchBar searchBar) => searchBar.Text = string.Empty;

        [RelayCommand]
        async Task PerformSearchAsync(string keyWord)
        {
            await cts.CancelAsync();
            cts = new();
            var token = cts.Token;
            try
            {
                var selectedTopicNames = await Task.Run(() =>
                {
                    // Get all names
                    List<string> names = GetNames();

                    // Perform case-insensitive search
                    List<string> result = names.Where(name => name.StartsWith(keyWord, StringComparison.OrdinalIgnoreCase)).ToList();

                    if (!result.SequenceEqual(SelectedTopicNames))
                    {
                        return result;
                    }
                    return null;
                });
                token.ThrowIfCancellationRequested();
                if (selectedTopicNames != null)
                {
                    SelectedTopicNames = selectedTopicNames;
                }
            }
            catch (OperationCanceledException) { }
        }


        // Commands      
        [RelayCommand]
        async Task NavigateAsync(string butText)
        {
            IsLoading = true;
            // Select the topic and load data from file
            topics.SelectedTopicIdx = topics.TopicsData.FindIndex(element => element.Name == butText);
            await highlightJs.InitAsync();
            await quiz.InitAsync();
            await Shell.Current.GoToAsync($"{nameof(QuestionPage)}");
            IsLoading = false;
        }
    }
}
