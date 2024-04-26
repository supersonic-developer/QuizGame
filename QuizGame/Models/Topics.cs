using System.Collections.ObjectModel;

namespace QuizGame.Models
{
    public class Topics
    {
        // Properties
        public List<(string Path, string Name)> Topic { get; }
        public string TopicsFile { get; }

        public Topics()
        {
            Topic = new List<(string, string)>();
            TopicsFile = "topics.txt";
        }
    }
}
