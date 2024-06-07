using Microsoft.Extensions.Logging;
using QuizGame.ViewModels;

namespace QuizGame.Views
{
    public partial class QuestionPage : ContentPage
    {
        public QuestionPage(QuestionPageViewModel questionPageViewModel)
        {
            InitializeComponent();

            // Set binding context
            BindingContext = questionPageViewModel;
        }
    }

}