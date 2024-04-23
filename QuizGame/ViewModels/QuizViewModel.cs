using Microsoft.VisualStudio.PlatformUI;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public class QuizViewModel : ObservableObject
    {
        // Member variable
        private Random rnd;
        private Quiz game;
        private Question selectedQuestion;

        // Property
        public Question SelectedQuestion 
        {
            get => selectedQuestion;
            set => SetProperty(ref selectedQuestion, value);
        }

        // Constructor
        public QuizViewModel(Quiz game) 
        { 
            rnd = new Random();
            this.game = game;
            RandomlySelectQuestion();
        }

        public void RandomlySelectQuestion()
        {
            int ID = rnd.Next(0, game.Questions.Count-1);
            SelectedQuestion = game.Questions[ID];
            game.Questions.RemoveAt(ID);
        }
    }
}
