using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Threading;
using QuizGame.Services.Interfaces;

namespace QuizGame.Services.Implementations
{
    public class AsyncInitializeHighlightJsService(IFileReaderService fileReaderService) : IAsyncInitializeService<(string, string, string)>
    {
        // File paths
        readonly string highlightJsPath = @"highlight.js\highlight.min.js";
        readonly string lightStyleCssPath = @"highlight.js\stackoverflow-light.min.css";
        readonly string darkStyleCssPath = @"highlight.js\stackoverflow-dark.min.css";

        // File reader service
        readonly IFileReaderService fileReaderService = fileReaderService;

        public async Task<(string, string, string)> InitializeAsync()
        {
            string HighlightJs = await fileReaderService.ReadFileAsync(highlightJsPath);
            string LightStyleCss = await fileReaderService.ReadFileAsync(lightStyleCssPath);
            string DarkStyleCss = await fileReaderService.ReadFileAsync(darkStyleCssPath);
            return (HighlightJs, LightStyleCss, DarkStyleCss);
        }

    }
}
