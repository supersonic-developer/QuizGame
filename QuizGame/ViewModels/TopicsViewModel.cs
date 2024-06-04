using QuizGame.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public partial class TopicsViewModel : ObservableObject
    {
        // Models and services
        readonly Topics topics;
        readonly IAsyncInitializeService<List<Question>> questionsService;
        readonly List<Question> questions;

        // Bindable properties
        [ObservableProperty]
        List<string> selectedTopicNames;

        [ObservableProperty]
        bool isLoading = false;

        // Cancellation token source for asynchronous search
        CancellationTokenSource cts = new();
        

        // Constructor
        public TopicsViewModel(Topics topics, IAsyncInitializeService<List<Question>> questionsService, List<Question> questions)
        {
            // Set topics data
            this.topics = topics;
            SelectedTopicNames = GetNames();
            topics.PropertyChanged += (s, e) => { SelectedTopicNames = GetNames(); };

            this.questionsService = questionsService;
            this.questions = questions;
        }

        // Method to query all topic names
        List<string> GetNames() => topics.TopicsData.Select(element => element.Name).ToList();

        
        // Commands
        [RelayCommand]
        static void Disappearing(SearchBar searchBar) => searchBar.Text = string.Empty;

        [RelayCommand]
        async Task PerformSearchAsync(string keyWord)
        {
            await cts.CancelAsync();
            cts = new();
            // Fire and forget a task to search for the keyword
            _ = Task.Run(() => 
            {
                try 
                {
                    // Get all names
                    List<string> names = GetNames();

                    // Perform case-insensitive search and add matching names to SelectedNames
                    List<string> result = names.Where(name => 
                    {
                        cts.Token.ThrowIfCancellationRequested();
                        return name.StartsWith(keyWord, StringComparison.OrdinalIgnoreCase);
                    }).ToList();

                    if (!result.SequenceEqual(SelectedTopicNames))
                    {
                        SelectedTopicNames = result;
                    }
                }
                catch(OperationCanceledException) { }
            });
        }

        [RelayCommand]
        async Task NavigateAsync(string butText)
        {
            IsLoading = true;
            topics.SelectedTopicIdx = topics.TopicsData.FindIndex(element => element.Name == butText);
            List<Question> readQuestions = await questionsService.InitializeAsync();
            questions.Clear();
            questions.AddRange(readQuestions);
            await Shell.Current.GoToAsync($"{nameof(QuizPage)}");
            IsLoading = false;
        }     
    }
}
