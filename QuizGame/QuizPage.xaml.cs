using System.Security.Cryptography.X509Certificates;
using QuizGame.ViewModels;

namespace QuizGame;

public partial class QuizPage : ContentPage
{
	public QuizViewModel QuizViewModel { get; set; }
	public QuizPage(QuizViewModel quizViewModel)
	{
		InitializeComponent();
		QuizViewModel = quizViewModel;
	}
}