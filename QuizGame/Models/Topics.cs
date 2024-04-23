using System.Collections.ObjectModel;

namespace QuizGame.Models
{
    public class Topics
    {
        // Properties
        public List<string> TopicPaths { get; }
        public string TopicsFile { get; }

        public Topics()
        {
            TopicPaths = new List<string>();
            TopicsFile = "topics.txt";
        }
    }
}
