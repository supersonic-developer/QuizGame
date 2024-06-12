using static System.Net.Mime.MediaTypeNames;

namespace QuizGame.Models
{
    public class Reference(List<(string, string)> links, string? text = null, CodeSnippet? codeSnippet = null)
    {
        public string? Text { get; set; } = text;
        public CodeSnippet? CodeBlock { get; set; } = codeSnippet;

        public List<(string Url, string Text)> Links { get; set; } = links;
    }
}
