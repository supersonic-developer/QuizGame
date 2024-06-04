using System.Globalization;

namespace QuizGame.Converters
{
    public class IsAnswerDisplay : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo cultureInfo)
        {
            if ((bool)value!)
            {
                return Colors.Green;
            }
            else
            {
                switch (Application.Current?.RequestedTheme)
                {
                    case AppTheme.Light:
                        return Color.FromRgb(80, 43, 212);
                    case AppTheme.Dark:
                        return Colors.Black;
                    default:
                        return Colors.White;
                }
            }
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo cultureInfo)
        {
            throw new NotImplementedException();
        }
    }
}
