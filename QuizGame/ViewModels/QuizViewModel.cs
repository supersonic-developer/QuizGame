using QuizGame.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using QuizGame.Helpers;

namespace QuizGame.ViewModels
{
    [QueryProperty(nameof(TargetPath), nameof(TargetPath))]
    public partial class QuizViewModel : ObservableObject
    {
        // Member variables
        readonly Random rnd;
        List<Question> game;

        [ObservableProperty]
        Question? selectedQuestion;

        [ObservableProperty]
        string? targetPath;
                            
        [ObservableProperty]
        HtmlWebViewSource htmlWebViewSource;

        [ObservableProperty]
        Visibility codeVisibility;

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
        // Constructor
        public QuizViewModel() 
        { 
            rnd = new Random();
            game = [];
            CodeVisibility = Visibility.Hidden;
            HtmlWebViewSource = new HtmlWebViewSource { Html = htmlData };
        }

        // Methods

        [RelayCommand]
        async Task AppearingAsync()
        {
            game = await MarkdownParser.ParseQuestionsAsync(TargetPath ?? throw new Exception("Query property was not available."));
            RandomlySelectQuestion();
            if (SelectedQuestion?.CodeBlock != null)
            {
                CodeVisibility = Visibility.Visible;
                HtmlWebViewSource.Html += WrapCode(SelectedQuestion.CodeBlock);
            }
        }

        public void RandomlySelectQuestion()
        {
            int ID = rnd.Next(0, game.Count-1);
            SelectedQuestion = game[ID];
            game.RemoveAt(ID);
        }

        string WrapCode(CodeSnippet codeInfo) => $"<pre><code class=\"language-" + codeInfo.Language + "\">" + codeInfo.Content + "</code></pre>";
    }
}
