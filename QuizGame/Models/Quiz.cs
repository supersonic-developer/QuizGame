using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Quiz
    {
        public List<Question> Questions { get; set; }

        public Quiz(List<Question> questions) 
        { 
            Questions = questions; 
        }
    }
}
