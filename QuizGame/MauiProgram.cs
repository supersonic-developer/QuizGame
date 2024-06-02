using Microsoft.Extensions.Logging;
using QuizGame.Models;
using QuizGame.ViewModels;
using CommunityToolkit.Maui;
using QuizGame.Services.Interfaces;
using QuizGame.Services.Implementations;
using QuizGame.Helpers;

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
                .UseMauiCommunityToolkit()
                .ConfigureServices();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }


        // Services configuration methods

        static void ConfigureServices(this MauiAppBuilder builder)
        {
            builder.Services.AddServices()
                            .AddViewModels()
                            .AddViews();
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

            return services;
        }

        static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainPageViewModel>();
            services.AddTransient<QuizViewModel>();
            return services;
        }

        static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>();
            services.AddTransient<QuizPage>();
            return services;
        }
    }
}
