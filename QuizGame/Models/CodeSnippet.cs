using CommunityToolkit.Mvvm.ComponentModel;
using QuizGame.Services.Interfaces;
using QuizGame.Helpers;
using Microsoft.VisualStudio.Threading;

namespace QuizGame.Models
{
    public class CodeSnippet(string language, string content)
    {
        // Properties
        public string Language { get; } = language;

        public string Content { get; } = content;
    }
}
