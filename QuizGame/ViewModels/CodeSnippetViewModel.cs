using CommunityToolkit.Mvvm.ComponentModel;
using QuizGame.Helpers;
using QuizGame.Models;
using QuizGame.Services.Interfaces;

namespace QuizGame.ViewModels
{
    public partial class CodeSnippetViewModel : ObservableObject, IAsyncInitializeService<string>
    {
        // Member variables
        readonly CodeSnippet? codeSnippet;
        readonly HighlightJs? libraries;

        // Properties
        [ObservableProperty]
        public string? html2Display;

        [ObservableProperty]
        bool isVisible;

        // Constructor
        public CodeSnippetViewModel(CodeSnippet? codeSnippet, HighlightJs libraries)
        {
            if (codeSnippet == null)
            {
                IsVisible = false;
            }
            else
            {
                this.codeSnippet = codeSnippet;
                this.libraries = libraries;
                IsVisible = true;
                _ = Task.Run(async () => Html2Display = await InitializeAsync());
                Application.Current!.RequestedThemeChanged += (s, e) => _ = Task.Run(async () => Html2Display = await InitializeAsync());
            }
        }


        // Methods
        public async Task<string> InitializeAsync()
        {
            // await libraries
            (string highlightJs, string lightStyleCss, string darkStyleCss) = await libraries!.Libraries;

            // Construct the HTML string
            string styleString = Application.Current?.RequestedTheme == AppTheme.Light ? "<style>" + lightStyleCss + "</style>" : "<style>" + darkStyleCss + "</style>";
            string scriptString = "<script>" + highlightJs + "</script>";
            string commandString = "<script>hljs.highlightAll();</script>";
            string codeString = $"<pre><code class=\"language-" + codeSnippet!.Language + "\">" + codeSnippet!.Content + "</code></pre>";

            return styleString + scriptString + codeString + commandString;
        }
    }
}
