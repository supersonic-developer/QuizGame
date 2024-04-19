using Microsoft.Extensions.Logging;

namespace QuizGame
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Add services
            //string path = "C:\\Users\\Zsombor\\git-repos\\QuizGame\\QuizGame\\quiz-assessments\\c++\\c++-quiz.md";
            //string path = "C:\\Users\\Zsombor\\git-repos\\QuizGame\\QuizGame\\quiz-assessments\\c-(programming-language)\\c-(programming-language)-quiz.md";
            string path = "C:\\Users\\Zsombor\\git-repos\\QuizGame\\QuizGame\\quiz-assessments\\adobe-xd\\adobe-xd-quiz.md";
            Models.MarkdownParser.ParseQuestions(path);
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
