using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Question
    {
        // static variable to generate unique IDs
        private static int nextID = 1;

        // Constructor
        public Question(string text) 
        {
            ID = nextID++;
            Text = text;
            Answers = new List<Answer>();
        }

        // Properties
        public int ID { get; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
        public List<CodeBlock>? CodeBlocks { get; set; }
        public string? ImageFilePath { get; set; }
    }
}
