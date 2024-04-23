using QuizGame.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using static System.Net.Mime.MediaTypeNames;

namespace QuizGame
{
    public partial class MainPage : ContentPage
    {
        // Member variable
        private MainPageViewModel mainPageViewModel;

        // Constructor
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            BindingContext = mainPageViewModel;
            InitializeComponent();
            this.mainPageViewModel = mainPageViewModel;
        }

        // Methods
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await mainPageViewModel.ReadTopicsAsync();
        }

        public void OnSearchBarTextChanged(object sender, TextChangedEventArgs e) => mainPageViewModel.PerformSearch(e.NewTextValue);
    }

}
