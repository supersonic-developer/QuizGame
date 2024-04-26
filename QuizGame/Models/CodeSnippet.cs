using CommunityToolkit.Mvvm.ComponentModel;

namespace QuizGame.Models
{
    public partial class CodeSnippet : ObservableObject
    {
        // Properties
        [ObservableProperty]
        string language;

        [ObservableProperty]
        string content;

        // Parametrizable constructor
        public CodeSnippet(string language, string content) 
        {
            Language = language;
            Content = content;
        }
    }
}
