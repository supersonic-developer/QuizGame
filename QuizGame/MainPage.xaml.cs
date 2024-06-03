using QuizGame.ViewModels;

namespace QuizGame
{
    public partial class MainPage : ContentPage
    {
        // Constructor
        public MainPage(TopicsViewModel topicsViewModel, HeaderViewModel headerViewModel)
        {
            InitializeComponent();
            // Set header data
            headerViewModel.Title = "Linked";
            headerViewModel.Subtitle = "Skill assessment quizzes";
            headerViewModel.ImagePath = "linkedin_logo.png";

            // Set binding contexts
            headerView.BindingContext = headerViewModel;
            topicsView.BindingContext = topicsViewModel;
        }
    }
}
