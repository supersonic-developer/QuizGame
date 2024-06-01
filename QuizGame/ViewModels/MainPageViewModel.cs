using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QuizGame.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        // Member variable
        readonly Topics topics;

        [ObservableProperty]
        List<string> selectedTopicNames;

        // Constructor
        public MainPageViewModel(Topics topics)
        {
            this.topics = topics;
            selectedTopicNames = [];
        }

        // Methods

        [RelayCommand]
        async Task AppearingAsync()
        {
            // Data was already read in from file, just set page default
            if (topics.TopicsData.Count > 0)
            {
                return;
            }
            else
            {
                using Stream stream = await FileSystem.OpenAppPackageFileAsync(topics.TopicsFile);
                using StreamReader reader = new StreamReader(stream);
                string content = await reader.ReadToEndAsync();
                SplitContent2Collection(content);
            }
        }

        [RelayCommand]
        void Disappearing(SearchBar searchBar) => searchBar.Text = string.Empty;

        [RelayCommand]
        void PerformSearch(string keyWord)
        {
            // Perform case-insensitive search and add matching names to SelectedNames
            List<string> searchResult = topics.TopicsData.Select(element => element.Name)
                                                    .Where(name => name.StartsWith(keyWord, StringComparison.OrdinalIgnoreCase)).ToList();
            // Reinitialize SelectedTopics only if current result differ from it
            if (!Enumerable.SequenceEqual(searchResult, SelectedTopicNames))
            {
                SelectedTopicNames = searchResult;
            }
        }

        [RelayCommand]
        async Task NavigateAsync(string butText)
        {
            var result = topics.TopicsData.Find(element => element.Name == butText);
            await Shell.Current.GoToAsync($"{nameof(QuizPage)}?TargetPath={Uri.EscapeDataString(result.Path)}");
        }

        void SplitContent2Collection(string content)
        {
            string[] words = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words) 
            {
                string path = word + @"\" + word + "-quiz.md";
                // Format name
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                string formattedName = textInfo.ToTitleCase(word.Replace("-", " "));
                // Add to list
                topics.TopicsData.Add((path, formattedName));
            }
            SelectedTopicNames = topics.TopicsData.Select(element => element.Name).ToList();
        }

     
    }
}
