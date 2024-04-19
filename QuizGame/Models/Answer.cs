using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Answer
    {
        public Answer(string text, bool isCorrect) 
        { 
            Text = text;
            IsCorrect = isCorrect;
        }

        public string Text { get; set; }
        public bool IsCorrect { get; set; }

        public List<CodeBlock>? CodeBlocks { get; set; }
    }
}
