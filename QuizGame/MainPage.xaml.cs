using QuizGame.ViewModels;

namespace QuizGame
{
    public partial class MainPage : ContentPage
    {
        // Constructor
        public MainPage(TopicsViewModel topicsViewModel)
        {
            InitializeComponent();

            // Set binding contexts
            topicsView.BindingContext = topicsViewModel;
        }
    }
}
