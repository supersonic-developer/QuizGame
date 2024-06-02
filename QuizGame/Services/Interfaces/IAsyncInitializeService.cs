
namespace QuizGame.Services.Interfaces
{
    public interface IAsyncInitializeService<T>
    {
        public Task<T> InitializeAsync();
    }
}
