using QuizGame.ViewModels;

namespace QuizGame
{
    public partial class AppShell : Shell
    {
        public AppShell(HeaderViewModel headerViewModel)
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(QuizPage), typeof(QuizPage));
            headerView.BindingContext = headerViewModel;
        }
    }
}
