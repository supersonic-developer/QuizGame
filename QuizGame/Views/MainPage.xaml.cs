using QuizGame.ViewModels;

namespace QuizGame.Views
{
    public partial class MainPage : ContentPage
    {
        // Constructor
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();

            // Set binding contexts
            BindingContext = mainPageViewModel;
        }
    }
}
