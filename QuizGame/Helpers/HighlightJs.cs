using Microsoft.VisualStudio.Threading;
using QuizGame.Services.Interfaces;

namespace QuizGame.Helpers
{
    public class HighlightJs(IAsyncInitializeService<(string, string, string)> highlightJsInitializer)
    {
        public JoinableTask<(string HighlightJs, string LightStyleCss, string DarkStyleCss)> Libraries { get; } = JoinableTaskContextHelper.Factory.RunAsync(highlightJsInitializer.InitializeAsync);
    }
}
