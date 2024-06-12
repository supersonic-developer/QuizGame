
namespace QuizGame.Models
{
    public class Question(string text, List<Answer> answers, CodeSnippet? codeSnippet = null, string? imagePath = null, Reference? reference = null)
    {
        // Properties
        public string Text { get; set; } = text;

        public List<Answer> Answers { get; set; } = answers;

        public CodeSnippet? CodeBlock { get; set; } = codeSnippet;

        public string? ImagePath { get; set; } = imagePath;

        public Reference? Reference { get; set; } = reference;
    }
}
