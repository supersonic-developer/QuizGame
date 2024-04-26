using CommunityToolkit.Mvvm.ComponentModel;


namespace QuizGame.Models
{
    public partial class Question : ObservableObject
    {
        // Properties
        [ObservableProperty]
        string text;

        [ObservableProperty]
        List<Answer> answers;

        [ObservableProperty]
        CodeSnippet? codeBlock;

        [ObservableProperty]
        string? imagePath;

        // Constructor
        public Question(string text) 
        {
            Text = text;
            Answers = [];
        }
    }
}
