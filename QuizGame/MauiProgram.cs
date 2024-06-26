﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using QuizGame.Handlers;
using QuizGame.Helpers;
using QuizGame.Models;
using QuizGame.Services.Implementations;
using QuizGame.Services.Interfaces;
using QuizGame.ViewModels;
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
                .UseMauiCommunityToolkit()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler<SearchBar, SearchBarExHandler>();
                });

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
            // Dialog service for async dialog displaying
            services.AddSingleton<IDialogService, DialogService>();

            // Topics service
            services.AddSingleton<IAsyncInitializeService<List<(string, string)>>, AsyncInitilizeTopicsService>();
            services.AddSingleton<Topics>();

            // HighlightJs service
            services.AddSingleton<IAsyncInitializeService<(string, string, string)>, AsyncInitializeHighlightJsService>();
            services.AddSingleton<HighlightJs>();

            // Questions service
            services.AddSingleton<IAsyncInitializeService<List<Question>>, AsyncInitializeQuestionsService>();
            services.AddSingleton<Quiz>();

            return services;
        }

        static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddSingleton<HeaderViewModel>();
            services.AddSingleton<MainPageViewModel>();
            services.AddTransient<QuestionPageViewModel>();
            services.AddTransient<ReferenceViewModel>();

            return services;
        }

        static IServiceCollection AddViews(this IServiceCollection services)
        {
            // Main page
            services.AddSingleton<MainPage>();
            // Question page
            services.AddTransient<QuestionPage>();

            return services;
        }
    }
}
