using QuizGame.ViewModels;
using QuizGame.Views;

namespace QuizGame
{
    public partial class AppShell : Shell
    {
        public AppShell(HeaderViewModel headerViewModel)
        {
            InitializeComponent();

            // Register route to QuestionPage
            Routing.RegisterRoute(nameof(QuestionPage), typeof(QuestionPage));
            headerView.BindingContext = headerViewModel;
        }
    }
}
