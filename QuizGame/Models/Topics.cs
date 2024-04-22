using System.Collections.ObjectModel;

namespace QuizGame.Models
{
    public class Topics
    {
        // Properties
        public ObservableCollection<string> TopicsNames { get; }
        public List<string> TopicsPaths { get; }
        public string TopicsFile { get; }

        public Topics()
        {
            TopicsNames = new ObservableCollection<string>();
            TopicsPaths = new List<string>();
            TopicsFile = "topics.txt";
        }
    }
}
