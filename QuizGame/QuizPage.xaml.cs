using QuizGame.ViewModels;
using QuizGame.Models;

namespace QuizGame;

public partial class QuizPage : ContentPage
{
	
	public QuizPage(Topics topics, QuestionViewModel questionViewModel)
	{
		InitializeComponent();
        // Set title
        Title = topics.SelectedTopicIdx.HasValue? topics.TopicsData[topics.SelectedTopicIdx.Value].Name.Replace("-", " ") + " Quizzes" : "";
        
        // Set binding context
        questionView.BindingContext = questionViewModel;
    }
}