using QuizGame.Helpers;
using QuizGame.Models;
using QuizGame.Services.Interfaces;

namespace QuizGame.Services.Implementations
{
    public class AsyncInitializeQuestionsService(IFileReaderService fileReaderService, Topics topics) : IAsyncInitializeService<List<Question>>
    {
        // File reader service
        readonly IFileReaderService fileReaderService = fileReaderService;
        // Topics
        readonly Topics topics = topics;

        public async Task<List<Question>> InitializeAsync()
        {
            // Output 
            List<Question> result = [];

            string path = topics.TopicsData!.Select(element => element.Path).ToList()[topics.SelectedTopicIdx!.Value];
            // Read file content asynchronously
            string content = await fileReaderService.ReadFileAsync(path);
            // Parse the content
            MarkdowParser parser = new(Path.GetDirectoryName(path)?.Replace(@"\", "/") ?? throw new Exception($"No directory was found for image at path:{path}"));
            result = parser.ParseQuestions(content);

            return result;
        }


    }
}
