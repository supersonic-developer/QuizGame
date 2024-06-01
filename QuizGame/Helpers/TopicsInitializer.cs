using Microsoft.VisualStudio.Threading;
using QuizGame.Models;
using System.Globalization;

namespace QuizGame.Helpers
{
    public class TopicsInitializer : BaseModelInitializer
    {
        // Path for topics file
        readonly string topicsFilePath = @"linkedin-skill-assessments-quizzes\topics.txt";

        // Separators in file
        readonly char[] separator = ['\r', '\n'];

        // Target object of initialize
        Topics topics;

        // Async lazy object to run initialization asynchronously
        AsyncLazy<List<(string, string)>> AsyncLazyTopicsReader { get; }

        // Constructor
        public TopicsInitializer(Topics topics)
        {
            this.topics = topics;
            AsyncLazyTopicsReader = new (ReadPathAndNameDataAsync, new JoinableTaskContext().Factory);
            _ = AsyncLazyTopicsReader.GetValueAsync().ContinueWith(async t => 
            {
                topics.TopicsData = await AsyncLazyTopicsReader.GetValueAsync();
            }, 
            TaskScheduler.Default);
        } 

        public async Task<List<(string, string)>> ReadPathAndNameDataAsync()
        {
            // output 
            List<(string, string)> result = [];

            // Read file content asynchronously
            string content = await LoadFileAsync(topicsFilePath);
            string[] words = content.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            // Build data
            foreach (string word in words)
            {
                string path = @"linkedin-skill-assessments-quizzes\" + word + @"\" + word + "-quiz.md";
                // Format name
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                string formattedName = textInfo.ToTitleCase(word.Replace("-", " "));
                // Add to list
                result.Add((path, formattedName));
            }

            return result;
        }
    }
}
