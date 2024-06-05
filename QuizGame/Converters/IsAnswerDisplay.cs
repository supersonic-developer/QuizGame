using QuizGame.ViewModels;
using System.Globalization;

namespace QuizGame.Converters
{
    public class IsAnswerDisplay : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo cultureInfo)
        {
            AnswerViewModel.State state = (AnswerViewModel.State)value!;
            return state switch
            {
                AnswerViewModel.State.LightTheme => Color.FromArgb("#512BD4"),
                AnswerViewModel.State.DarkTheme => Colors.Black,
                AnswerViewModel.State.IsSelected => Colors.LightSkyBlue,
                AnswerViewModel.State.InCorrectDisplayed => Colors.Red,
                AnswerViewModel.State.CorrectDisplayed => Colors.Green,
                _ => throw new NotImplementedException(),
            };
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
