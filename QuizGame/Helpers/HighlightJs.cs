using QuizGame.Services.Interfaces;

namespace QuizGame.Helpers
{
    public class HighlightJs(IAsyncInitializeService<(string, string, string)> highlightJsInitializer)
    {
        public (string HighlightJs, string LightStyleCss, string DarkStyleCss)? Libraries { get; private set; }

        public async Task InitAsync() => Libraries ??= await highlightJsInitializer.InitializeAsync(); 
    }
}
