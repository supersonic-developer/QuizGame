using Microsoft.Extensions.Logging;
using QuizGame.Models;
using QuizGame.ViewModels;
using CommunityToolkit.Maui;

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
                })
                .UseMauiCommunityToolkit();

            // Add services
            builder.Services.AddSingleton<Topics>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddTransient<QuizViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<QuizPage>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
