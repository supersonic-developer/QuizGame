using System.Globalization;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace QuizGame.Converters
{
    public sealed class TaskResultConverter : IValueConverter
    {

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo language)
        {
            if (value is Task task)
            {
                if (task.IsCompletedSuccessfully)
                {
                    var resultProperty = task.GetType().GetProperty("Result");
                    return resultProperty?.GetValue(task);
                }
                else 
                {
                    var resultType = task.GetType().GetGenericArguments()[0];
                    return GetDefault(resultType);
                }
            }

            return null;
        }


        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo language) => throw new NotImplementedException();


        private static object? GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}
