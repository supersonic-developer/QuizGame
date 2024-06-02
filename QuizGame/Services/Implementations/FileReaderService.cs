using QuizGame.Services.Interfaces;

namespace QuizGame.Services.Implementations
{
    public class FileReaderService : IFileReaderService
    {
        public async Task<string> ReadFileAsync(string path)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(path);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}
