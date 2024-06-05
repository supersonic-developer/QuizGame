using QuizGame.ViewModels;

namespace QuizGame;

public partial class QuizPage : ContentPage
{

    public QuizPage(QuizPageViewModel quizPageViewModel)
    {
        InitializeComponent();

        // Set binding context
        questionView.BindingContext = quizPageViewModel.QuestionViewModel;
        headerView.BindingContext = quizPageViewModel.HeaderViewModel;
    }
}