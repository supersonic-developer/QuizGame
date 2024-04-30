using CommunityToolkit.Mvvm.ComponentModel;
using QuizGame.Models;

namespace QuizGame.ViewModels
{
    public partial class CodeSnippetViewModel : ObservableObject
    {
        CodeSnippet? codeSnippet;

        readonly string htmlData = @"<link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/styles/default.min.css"">
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/highlight.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/bash.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/cpp.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/c.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/css.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/django.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/go.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/java.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/javascript.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/json.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/kotlin.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/matlab.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/objectivec.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/php.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/python.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/r.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/ruby.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/rust.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/scala.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/swift.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/tsql.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/vba.min.js""></script>
                                    <script src=""https://cdnjs.cloudflare.com/ajax/libs/highlight.js/11.9.0/languages/xml.min.js""></script>
                                    <script>hljs.highlightAll();</script>
                                    ";

        [ObservableProperty]
        string html2Display;

        [ObservableProperty]
        bool isVisible;

        [ObservableProperty]
        int width;

        [ObservableProperty]
        int height;

        public CodeSnippetViewModel(CodeSnippet? codeSnippet)
        {
            if (codeSnippet == null)
            {
                IsVisible = false;
                html2Display = string.Empty;
                width = 0;
                height = 0;
            }
            else
            {
                this.codeSnippet = codeSnippet;
                IsVisible = true;
                Html2Display = htmlData + WrapCode();
                codeSnippet.PropertyChanged += CodeSnippet_PropertyChanged;
            }
        }

        private void CodeSnippet_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) => Html2Display = htmlData + WrapCode();

        string WrapCode()
        {
            CalculateSize();
            return $"<pre><code class=\"language-" + codeSnippet?.Language + "\">" + codeSnippet?.Content + "</code></pre>";
        }

        void CalculateSize()
        {
            string[] lines = codeSnippet?.Content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None) ?? throw new Exception("bsg");

            int maxCharacters = 0;

            foreach (string line in lines)
            {
                if (line.Length > maxCharacters)
                {
                    maxCharacters = line.Length;
                }
            }

            Width = 10*maxCharacters + 15;
            Height = 15*lines.Length + 50;
        }
    }
}
