using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.Threading;
using QuizGame.Helpers;

namespace QuizGame.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        // Member variable
        readonly Topics topics;

        [ObservableProperty]
        public List<string> selectedTopicNames;

        // Constructor
        public MainPageViewModel(Topics topics)
        {
            selectedTopicNames = [];
            this.topics = topics;
            _ = Task.Run(InitAsync);
        }

        async Task InitAsync() => SelectedTopicNames = await GetNamesAsync();

        async Task<List<string>> GetNamesAsync()
        {
            List<(string Path, string Name)> data = await topics.TopicsData;
            return data.Select(element => element.Name).ToList();
        }

        // Methods
        [RelayCommand]
        static void Disappearing(SearchBar searchBar) => searchBar.Text = string.Empty;

        [RelayCommand]
        async Task PerformSearchAsync(string keyWord)
        {
            // Get all names
            List<string> names = await GetNamesAsync();

            // Perform case-insensitive search and add matching names to SelectedNames
            List<string> result = names.Where(name => name.StartsWith(keyWord, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!result.Equals(SelectedTopicNames))
                SelectedTopicNames = new List<string>(result);
        }

        [RelayCommand]
        async Task NavigateAsync(string butText)
        {
            var tupleList = await topics.TopicsData;
            var result = tupleList.Find(element => element.Name == butText);
            await Shell.Current.GoToAsync($"{nameof(QuizPage)}?TargetPath={Uri.EscapeDataString(result.Path)}");
        }     
    }
}
