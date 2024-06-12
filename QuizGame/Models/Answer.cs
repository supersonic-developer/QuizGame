
namespace QuizGame.Models
{
    public class Answer(string text, bool isCorrect, CodeSnippet? codeSnippet = null, string? imagePath = null)
    {
        public string Text { get; set; } = text;

        public bool IsCorrect { get; set; } = isCorrect;

        public CodeSnippet? CodeBlock { get; set; } = codeSnippet;

        public string? ImagePath { get; set; } = imagePath;
    }
}
