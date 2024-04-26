using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public partial class Answer : ObservableObject
    {
        [ObservableProperty]
        string text;

        [ObservableProperty]
        bool isCorrect;

        [ObservableProperty]
        CodeSnippet? codeBlock;

        public Answer(string text, bool isCorrect) 
        { 
            Text = text;
            IsCorrect = isCorrect;
        }
    }
}
