using QuizGame.ViewModels;

namespace QuizGame;

public partial class QuizPage : ContentPage
{
	
	public QuizPage(QuizViewModel quizViewModel)
	{
		InitializeComponent();
		BindingContext = quizViewModel;
    }
}