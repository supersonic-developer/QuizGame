using CommunityToolkit.Mvvm.ComponentModel;
using QuizGame.Services.Interfaces;

namespace QuizGame.Models
{
    public class Topics(IAsyncInitializeService<List<(string, string)>> topicsInitializer)
    {
        // Property
        public List<(string Path, string Name)>? TopicsData { get; private set; }

        public int? SelectedTopicIdx { get; set; }

        /* @brief Fire and forget a task to await the initialized data
         * @param topicsInitializer: The service to initialize the data
         * @retval Topics: the singleton instance of the Topics class
         */
        public async Task InitAsync() => TopicsData ??= await topicsInitializer.InitializeAsync();
    }
}
