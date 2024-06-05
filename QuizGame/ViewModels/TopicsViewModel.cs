using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuizGame.Helpers;
using QuizGame.Models;
using QuizGame.Services.Interfaces;

namespace QuizGame.ViewModels
{
    public partial class TopicsViewModel : ObservableObject
    {
        // Models and services
        readonly Topics topics;
        readonly List<Question> questions;
        readonly IAsyncInitializeService<List<Question>> questionsService;

        // Binded properties
        [ObservableProperty]
        List<string> selectedTopicNames;

        // Cancellation token source for asynchronous search
        CancellationTokenSource cts = new();


        // Constructor
        public TopicsViewModel(Topics topics, IAsyncInitializeService<List<Question>> questionsService, List<Question> questions)
        {
            this.questionsService = questionsService;
            this.questions = questions;
            this.topics = topics;

            // Add all name to the list (adding buttons on UI)
            SelectedTopicNames = GetNames();
            topics.PropertyChanged += (s, e) => SetNames();
        }


        // Methods
        List<string> GetNames() => topics.TopicsData.Select(element => element.Name).ToList();

        void SetNames() => SelectedTopicNames = topics.TopicsData.Select(element => element.Name).ToList();

        public async Task SearchAndSetElementsAsync(string keyWord)
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
            // Signal navigation start (turn on activity bar on UI)
            WeakReferenceMessenger.Default.Send(new NavigationRequestedMessage());

            // Select the topic and load data from file
            topics.SelectedTopicIdx = topics.TopicsData.FindIndex(element => element.Name == butText);
            questions.Clear();
            questions.AddRange(await questionsService.InitializeAsync());
            await Shell.Current.GoToAsync($"{nameof(QuizPage)}");

            // Signal navigation end (turn off activity bar on UI)
            WeakReferenceMessenger.Default.Send(new NavigationCompletedMessage());
        }
    }
}
