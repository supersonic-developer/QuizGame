namespace QuizGame
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Routing.RegisterRoute(nameof(QuizPage), typeof(QuizPage));
        }
    }
}
