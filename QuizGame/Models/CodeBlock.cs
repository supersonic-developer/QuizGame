using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class CodeBlock
    {
        // Parametrizable constructor
        public CodeBlock(string language, string content) 
        {
            Language = language;
            Content = content;
        }

        // Properties
        public string Language { get; set; }

        public string Content { get; set; }
    }
}
