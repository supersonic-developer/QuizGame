using QuizGame.ViewModels;

namespace QuizGame
{
    public partial class MainPage : ContentPage
    {
        // Constructor
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            BindingContext = mainPageViewModel;
            InitializeComponent();
        }
    }
}
