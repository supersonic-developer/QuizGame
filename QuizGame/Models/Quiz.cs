using QuizGame.Services.Interfaces;

namespace QuizGame.Models
{
    public class Quiz(IAsyncInitializeService<List<Question>> asyncInitializeQuestions)
    {
        public List<Question>? Questions { get; set; }

        public async Task InitAsync() => Questions ??= await asyncInitializeQuestions.InitializeAsync();
    }
}
