using QuizGame.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Helpers;

namespace QuizGame.ViewModels
{
    [QueryProperty(nameof(TargetPath), nameof(TargetPath))]
    public partial class QuizViewModel : ObservableObject
    {
        // Member variables
        readonly Random rnd;
        List<Question> game;

        [ObservableProperty]
        QuestionViewModel questionViewModel;

        [ObservableProperty]
        string? targetPath;
        
        // Constructor
        public QuizViewModel() 
        { 
            rnd = new Random();
            game = [];
            questionViewModel = new QuestionViewModel();
        }

        // Methods
        [RelayCommand]
        async Task AppearingAsync()
        {
            game = await MarkdownParser.ParseQuestionsAsync(TargetPath ?? throw new Exception("Query property was not available."));
            RandomlySelectQuestion();
        }

        public void RandomlySelectQuestion()
        {
            int ID = rnd.Next(0, game.Count-1);
            QuestionViewModel = new QuestionViewModel(game[ID]);
            game.RemoveAt(ID);
        }
    }
}
