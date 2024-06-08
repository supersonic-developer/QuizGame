using CommunityToolkit.Mvvm.ComponentModel;
using QuizGame.Helpers;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public partial class CodeSnippetViewModel : ObservableObject
    {
        // Member variables
        readonly CodeSnippet? codeSnippet;
        readonly HighlightJs? highlightJs;

        // Properties
        [ObservableProperty]
        public string? html2Display;

        [ObservableProperty]
        bool isVisible;

        // Constructor
        public CodeSnippetViewModel(CodeSnippet? codeSnippet, HighlightJs highlightJs)
        {
            if (codeSnippet == null)
            {
                IsVisible = false;
            }
            else
            {
                this.codeSnippet = codeSnippet;
                this.highlightJs = highlightJs;
                IsVisible = true;
                BuildHtmlString();
                Application.Current!.RequestedThemeChanged += (_, _) => BuildHtmlString();
            }
        }


        // Methods
        public void BuildHtmlString()
        {
            if (IsVisible)
            {
                // Construct the HTML string
                string styleString = Application.Current?.RequestedTheme == AppTheme.Light ? "<style>" + highlightJs!.Libraries!.Value.LightStyleCss + "</style>" : "<style>" + highlightJs!.Libraries!.Value.DarkStyleCss + "</style>";
                string scriptString = "<script>" + highlightJs!.Libraries!.Value.HighlightJs + "</script>";
                string commandString = "<script>hljs.highlightAll();</script>";
                string codeString = $"<pre><code class=\"language-" + codeSnippet!.Language + "\">" + codeSnippet!.Content + "</code></pre>";

                Html2Display = styleString + scriptString + commandString + codeString;
            }
        }
    }
}
