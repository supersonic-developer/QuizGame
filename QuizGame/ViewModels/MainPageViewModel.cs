using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using QuizGame.Helpers;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public class MainPageViewModel : ObservableObject
    {
        // Member variable
        private Topics topics;
        private List<string> topicNames;
        private List<string> selectedTopicNames;

        // Property
        public List<string> SelectedTopicNames 
        {
            get => selectedTopicNames;
            set => SetProperty(ref selectedTopicNames, value);
        }
       

        // Constructor
        public MainPageViewModel(Topics topics)
        {
            this.topics = topics;
            topicNames = new List<string>();
            selectedTopicNames = new List<string>();
        }
                
        // Methods
        public async Task ReadTopicsAsync()
        {
            using Stream stream = await FileSystem.OpenAppPackageFileAsync(topics.TopicsFile);
            using StreamReader reader = new StreamReader(stream);
            string content = await reader.ReadToEndAsync();
            SplitContent2Collection(content);
        }

        private void SplitContent2Collection(string content)
        {
            string[] words = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words) 
            {
                topics.TopicPaths.Add(word + "\\" + word + "-quiz.md");
                // Format name
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                string formattedName = textInfo.ToTitleCase(word.Replace("-", " "));
                topicNames.Add(formattedName);
            }
            SelectedTopicNames = new List<string>(topicNames);
        }

        public void PerformSearch(string keyWord)
        {
            // Perform case-insensitive search and add matching names to SelectedNames
            List<string> searchResult = [.. topicNames.Where(name => name.IndexOf(keyWord, StringComparison.OrdinalIgnoreCase) == 0)];
            // Reinitialize SelectedTopics only if current result differ from it
            if (!Enumerable.SequenceEqual(searchResult, SelectedTopicNames))
            {
                // Clear previous search results
                SelectedTopicNames.Clear();
                SelectedTopicNames = searchResult;
            }
        }
    }
}
