using System.Globalization;

namespace QuizGame.Converters
{
    public class IsAnswerDisplay : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo cultureInfo)
        {
            bool isAnswerDisplayed = (bool)value!;
            switch (Application.Current?.RequestedTheme)
            {
                case AppTheme.Light:
                    if (isAnswerDisplayed)
                    {
                        return Colors.Green;
                    }
                    else
                    {
                        return Color.FromRgb(80, 43, 212);
                    }
                    
                case AppTheme.Dark:
                    if (isAnswerDisplayed)
                    {
                        return Colors.Green;
                    }
                    else
                    {
                        return Colors.Black;
                    }
                default:
                    return Colors.White;
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
