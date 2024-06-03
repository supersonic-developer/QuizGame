
namespace QuizGame.Models
{
    public class CodeSnippet(string language, string content)
    {
        // Properties
        public string Language { get; set; } = language;

        public string Content { get; set; } = content;
    }
}
