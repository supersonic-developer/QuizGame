using QuizGame.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using static System.Net.Mime.MediaTypeNames;

namespace QuizGame
{
    public partial class MainPage : ContentPage
    {
        // Properties
        private static string topicsPath = "topics.txt";
        public MainPageViewModel MainPageViewModel { get; set; }

        // Constructor
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            MainPageViewModel = mainPageViewModel;
            Loaded += OnPageLoaded;
            MainPageViewModel.Topics.CollectionChanged += CreateButtons;
        }

        // Methods
        private async void OnPageLoaded(object? sender, EventArgs e) => await MainPageViewModel.ReadTopics(topicsPath);

        private void CreateButtons(object? sender, EventArgs e)
        {
            if (sender == MainPageViewModel.Topics)
            {
                CreateButton(MainPageViewModel.Topics[^1]);
            }
        }

        private void CreateButton(string text)
        {
            Button button = new Button();
            button.Text = text;
            butVerticalStackLayout.Children.Add(button);
        }
    }

}
