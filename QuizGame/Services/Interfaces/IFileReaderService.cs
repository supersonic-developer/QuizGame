namespace QuizGame.Services.Interfaces
{
    public interface IFileReaderService
    {
        public Task<string> ReadFileAsync(string path);
    }
}
