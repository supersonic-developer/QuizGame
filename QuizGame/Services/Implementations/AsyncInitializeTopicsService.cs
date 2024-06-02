using QuizGame.Services.Interfaces;
using System.Globalization;

namespace QuizGame.Services.Implementations
{
    public class AsyncInitilizeTopicsService(IFileReaderService fileReaderService) : IAsyncInitializeService<List<(string, string)>>
    {
        // Path for topics file
        readonly string topicsFilePath = @"linkedin-skill-assessments-quizzes\topics.txt";

        // Separators in file
        readonly char[] separator = ['\r', '\n'];

        // File reader service
        readonly IFileReaderService fileReaderService = fileReaderService;

        public async Task<List<(string, string)>> InitializeAsync()
        {
            // Output 
            List<(string, string)> result = [];

            // Read file content asynchronously
            string content = await fileReaderService.ReadFileAsync(topicsFilePath);
            string[] words = content.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            // Build data
            foreach (string word in words)
            {
                string path = @"linkedin-skill-assessments-quizzes\" + word + @"\" + word + "-quiz.md";
                // Format name
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                string formattedName = textInfo.ToTitleCase(word.Replace("-", " "));
                // Add to list
                result.Add((path, formattedName));
            }
            return result;
        }
    }
}
