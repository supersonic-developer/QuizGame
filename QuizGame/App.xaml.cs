using QuizGame.ViewModels;

namespace QuizGame
{
    public partial class App : Application
    {
        public App(HeaderViewModel headerViewModel)
        {
            InitializeComponent();

            MainPage = new AppShell(headerViewModel);
        }
    }
}
