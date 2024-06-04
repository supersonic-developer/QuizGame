using Microsoft.Extensions.Logging;
using QuizGame.Models;
using QuizGame.ViewModels;
using CommunityToolkit.Maui;
using QuizGame.Services.Interfaces;
using QuizGame.Services.Implementations;
using QuizGame.Helpers;
using QuizGame.Views;

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

            builder.ConfigureServices();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }


        // Services configuration methods

        static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddServices();
            builder.Services.AddViewModels();
            builder.Services.AddViews();

            return builder;
        }

        static IServiceCollection AddServices(this IServiceCollection services)
        {
            // File reader service for async file reading
            services.AddSingleton<IFileReaderService, FileReaderService>();

            // Topics service
            services.AddSingleton<IAsyncInitializeService<List<(string, string)>>, AsyncInitilizeTopicsService>();
            services.AddSingleton<Topics>();

            // HighlightJs service
            services.AddSingleton<IAsyncInitializeService<(string, string, string)>, AsyncInitializeHighlightJsService>();
            services.AddSingleton<HighlightJs>();

            // Questions service
            services.AddSingleton<IAsyncInitializeService<List<Question>>, AsyncInitializeQuestionsService>();
            services.AddSingleton<List<Question>>();

            return services;
        }

        static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<TopicsViewModel>();
            services.AddSingleton<HeaderViewModel>();
            services.AddTransient<QuestionViewModel>();

            return services;
        }

        static IServiceCollection AddViews(this IServiceCollection services)
        {
            // Main page
            services.AddSingleton<MainPage>();
            // Question page
            services.AddTransient<QuizPage>();

            return services;
        }
    }
}
