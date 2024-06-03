using CommunityToolkit.Mvvm.ComponentModel;
using QuizGame.Services.Interfaces;

namespace QuizGame.Models
{
    public partial class Topics : ObservableObject
    {
        // Property
        [ObservableProperty]
        List<(string Path, string Name)> topicsData = [];

        [ObservableProperty]
        int? selectedTopicIdx;

        /* @brief Fire and forget a task to await the initialized data
         * @param topicsInitializer: The service to initialize the data
         * @retval Topics: the singleton instance of the Topics class
         */
        public Topics(IAsyncInitializeService<List<(string, string)>> topicsInitializer) => _ = Task.Run(async () => TopicsData = await topicsInitializer.InitializeAsync());
    }
}
