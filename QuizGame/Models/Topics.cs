using Microsoft.VisualStudio.Threading;
using QuizGame.Helpers;
using QuizGame.Services.Interfaces;

namespace QuizGame.Models
{
    public class Topics(IAsyncInitializeService<List<(string, string)>> topicsInitializer)
    {
        // Property
        public JoinableTask<List<(string Path, string Name)>> TopicsData { get; } = JoinableTaskContextHelper.Factory.RunAsync(topicsInitializer.InitializeAsync);
    }
}
