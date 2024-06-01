using Microsoft.VisualStudio.Threading;
using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Helpers
{
    public class CodeSnippetInitializer : BaseModelInitializer
    {
        // File paths
        readonly string highlightJsPath = @"highlight.js\highlight.min.js";
        readonly string lightStylePath = @"highlight.js\stackoverflow-light.min.css";
        readonly string darkStylePath = @"highlight.js\stackoverflow-dark.min.css";


        // Async lazy object to run initialization asynchronously
        public AsyncLazy<(string HighlightJs, string LightStyleCss, string DarkStyleCss)> AsyncLazyHighLightLibrariesReader { get; }

        // Constructor
        public CodeSnippetInitializer() => AsyncLazyHighLightLibrariesReader = new(LoadLibrariesAsync, new JoinableTaskContext().Factory);

        public async Task<(string HighlightJs, string LightStyleCss, string DarkStyleCss)> LoadLibrariesAsync()
        {
            string HighlightJs = await LoadFileAsync(highlightJsPath);
            string LightStyleCss = await LoadFileAsync(lightStylePath);
            string DarkStyleCss = await LoadFileAsync(darkStylePath);
            return (HighlightJs, LightStyleCss, DarkStyleCss);
        }
    }
}
