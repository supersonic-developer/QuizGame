using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class CodeSnippet
    {
        // Properties
        public string Language { get; set; }
        public string Content { get; set; }

        // Parametrizable constructor
        public CodeSnippet(string language, string content) 
        {
            Language = language;
            Content = content;
        }
    }
}
