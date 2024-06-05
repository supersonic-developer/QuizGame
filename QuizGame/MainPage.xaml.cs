using QuizGame.ViewModels;

namespace QuizGame
{
    public partial class MainPage : ContentPage
    {
        // Constructor
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();

            // Set binding contexts
            BindingContext = mainPageViewModel;
            topicsView.BindingContext = mainPageViewModel.TopicsViewModel;
            headerView.BindingContext = mainPageViewModel.HeaderViewModel;
        }
    }
}
