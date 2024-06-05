using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public class QuizPageViewModel
    {
        public QuestionViewModel QuestionViewModel { get; }

        public HeaderViewModel HeaderViewModel { get; }

        public QuizPageViewModel(Topics topics, HeaderViewModel headerViewModel, QuestionViewModel questionViewModel)
        {
            QuestionViewModel = questionViewModel;
            HeaderViewModel = headerViewModel;
            SetHeader(headerViewModel, topics);
        }

        static void SetHeader(HeaderViewModel headerViewModel, Topics topics)
        {
            headerViewModel.Title = topics.TopicsData[(int)topics.SelectedTopicIdx!].Name;
            headerViewModel.Subtitle = "";
            headerViewModel.ImagePath = "";
            headerViewModel.HomeImagePath = "home_icon.png";
        }
    }
}
