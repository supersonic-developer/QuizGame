using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public class QuizVM
    {
        public QuizVM(Quiz game) 
        { 
            rnd = new Random();
            Game = game;
        }

        private Random rnd;

        public Quiz Game { get; set; }

        public Question RandomlySelectQ()
        {
            int ID = rnd.Next(0, Game.Questions.Count-1);
            return Game.Questions[ID];
        }
    }
}
