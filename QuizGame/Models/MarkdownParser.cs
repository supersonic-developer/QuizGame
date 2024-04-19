using Markdig;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace QuizGame.Models
{
    public static class MarkdownParser
    {
        public static List<Question> ParseQuestions(string filePath)
        {
            // Read in file as plain text
            string sourceText = File.ReadAllText(filePath);
            // Get directory of .md file
            string directory = Path.GetDirectoryName(filePath) ?? throw new Exception("Failed to get directory from file path.");

            // Parse the text 
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var document = Markdown.Parse(sourceText, pipeline);

            // Create the list for questions
            List<Question> quiz = new List<Question>();
            // Auxiliary variable for process
            bool isReadingQuestion = false;

            // Traverse the AST and process the content
            foreach (var block in document)
            {
                switch (block)
                {
                    case HeadingBlock:          
                        isReadingQuestion = true;
                        // Parse heading to question
                        Question? currentQuestion = ParseQuestion((HeadingBlock)block, quiz);
                        break;
                    case ListBlock:
                        isReadingQuestion = false;
                        // Add answer(s) to question
                        ParseAnswer((ListBlock)block, quiz[quiz.Count - 1]);
                        break;
                    case FencedCodeBlock:
                        // Parse code block and add to question or answer
                        ParseCodeSnippet((FencedCodeBlock)block, quiz[quiz.Count - 1], isReadingQuestion);
                        break;
                    case ParagraphBlock:
                        // Pictures that is required to use
                        ParseImages((ParagraphBlock)block, quiz[quiz.Count - 1], directory);
                        // Links for answers, currently I wont use
                        break;
                    case LinkReferenceDefinitionGroup:
                        // Still links just they didnt place it inside brackets
                        break;
                    default:
                        break;
                }
            }
            return quiz;
        }

        private static void ParseImages(ParagraphBlock paragraphBlock, Question currentQuestion, string rootDir)
        {
            if (paragraphBlock.Inline == null) 
                return;
            foreach (var descendant in paragraphBlock.Inline.Descendants())
            {
                if (descendant is Markdig.Syntax.Inlines.LinkInline linkInline)
                {
                    if (linkInline.IsImage)
                    {
                        currentQuestion.ImageFilePath = rootDir + "\\" + linkInline.Url?.Replace("/", "\\");
                    }
                }
            }
        }

        // Parse heading to question, if it is not a question (4 level heading) then returns null
        private static Question? ParseQuestion(HeadingBlock headingBlock, List<Question> quiz)
        {
            if (headingBlock.Level == 4)
            {
                if (headingBlock.Inline is null)
                    return null;
                string? question = null;
                foreach (var descendant in headingBlock.Inline.Descendants())
                {
                    question += descendant.ToString();
                }
                quiz.Add(new Question(question ?? throw new Exception("There is no question.")));
            }
            return null;
        }

        // Parse listblock to answer(s) and add it to the question
        private static void ParseAnswer(ListBlock listBlock, Question currentQuestion)
        {
            foreach (ListItemBlock listItemBlock in listBlock)
            {
                if (listItemBlock.LastChild is ParagraphBlock paragraphBlock)
                {
                    if (paragraphBlock.Inline?.FirstChild is Markdig.Extensions.TaskLists.TaskList taskList)
                    {
                        string answer = "";
                        foreach (var descendant in paragraphBlock.Inline.Descendants())
                        {
                            answer += descendant.ToString();
                        }
                        currentQuestion.Answers.Add(new Answer(answer, taskList.Checked));
                    }
                }
            }
        }

        private static void ParseCodeSnippet(FencedCodeBlock codeBlock, Question currentQuestion, bool isReadingQuestion)
        {
            string? language = codeBlock.Info;
            string? code = codeBlock.Lines.ToString();
            if (language != null && code != null)
            {
                if (isReadingQuestion)
                {
                    currentQuestion.CodeBlocks ??= new List<CodeBlock>();
                    currentQuestion.CodeBlocks.Add(new CodeBlock(language, code));
                }
                else 
                {
                    currentQuestion.Answers[currentQuestion.Answers.Count - 1].CodeBlocks ??= new List<CodeBlock>();
                    currentQuestion.Answers[currentQuestion.Answers.Count - 1].CodeBlocks.Add(new CodeBlock(language, code));
                }
            }
        }
    }
}
