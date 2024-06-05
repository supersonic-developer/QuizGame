using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using QuizGame.Helpers;

namespace QuizGame.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        // Binded properties
        [ObservableProperty]
        bool isLoading = false;

        // View models
        public HeaderViewModel HeaderViewModel { get; }
        public TopicsViewModel TopicsViewModel { get; }


        // Constructor
        public MainPageViewModel(HeaderViewModel headerViewModel, TopicsViewModel topicsViewModel)
        {
            HeaderViewModel = headerViewModel;
            TopicsViewModel = topicsViewModel;
            SetHeader();

            // Register messages
            WeakReferenceMessenger.Default.Register<NavigationRequestedMessage>(this, (_, _) => IsLoading = true);
            WeakReferenceMessenger.Default.Register<NavigationCompletedMessage>(this, (_, _) => IsLoading = false);
            WeakReferenceMessenger.Default.Register<UpdateHeaderMessage>(this, (_, _) => SetHeader());
        }


        // Methods
        void SetHeader()
        {
            HeaderViewModel.Title = "Linked";
            HeaderViewModel.Subtitle = " Quizzes";
            HeaderViewModel.ImagePath = "linkedin_logo.png";
            HeaderViewModel.HomeImagePath = "";
        }


        // Commands
        [RelayCommand]
        static void Disappearing(SearchBar searchBar) => searchBar.Text = string.Empty;

        [RelayCommand]
        async Task PerformSearchAsync(string keyWord) => await TopicsViewModel.SearchAndSetElementsAsync(keyWord);
    }
}
