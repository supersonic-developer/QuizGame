using System.Collections.ObjectModel;
using QuizGame.Helpers;

namespace QuizGame.ViewModels
{
    public class MainPageViewModel
    {
        // Property
        public ObservableCollection<string> Topics { get; set; }

        // Constructor
        public MainPageViewModel()
        {
            Topics = new ObservableCollection<string>();
        }
                
        // Methods
        public async Task ReadTopics(string fileName)
        {
            using Stream stream = await FileSystem.OpenAppPackageFileAsync(fileName);
            using StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();
            SplitContent2Collection(content);
        }

        private void SplitContent2Collection(string content)
        {
            string[] words = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words) 
            {
                MarkdownParser.TopicsPaths.Add(word + "\\" + word + "-quiz.md");
                Topics.Add(word);
            }
        }
    }
}
