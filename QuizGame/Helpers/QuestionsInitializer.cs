using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.VisualStudio.Threading;
using QuizGame.Models;

namespace QuizGame.Helpers
{
    public class QuestionsInitializer : BaseModelInitializer
    {
        // File path
        readonly string currentPath;

        // Async lazy object to run initialization asynchronously
        public AsyncLazy<List<Question>> AsyncLazyQuestionsReader { get; }

        // Constructor
        public QuestionsInitializer(string path)
        { 
            currentPath = path;
            AsyncLazyQuestionsReader = new(ParseQuestionsAsync, new JoinableTaskContext().Factory);
        } 

        // Parser functions
        public async Task<List<Question>> ParseQuestionsAsync()
        {
            // Read in file as plain text
            string sourceText = await LoadFileAsync(currentPath);

            // Get directory of .md file
            string directory = currentPath[..currentPath.LastIndexOf('\\')] ?? throw new Exception("Failed to get directory of given file path.");

            // Parse the text 
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var document = Markdown.Parse(sourceText, pipeline);

            // Create the list for questions
            List<Question> quiz = [];
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
                        ParseAnswer((ListBlock)block, quiz[^1]);
                        break;
                    case FencedCodeBlock:
                        // Parse code block and add to question or answer
                        QuestionsInitializer.ParseCodeSnippet((FencedCodeBlock)block, quiz[^1], isReadingQuestion);
                        break;
                    case ParagraphBlock:
                        // Pictures that is required to use
                        QuestionsInitializer.ParseImages((ParagraphBlock)block, quiz[^1], directory);
                        // Links for answers, currently I wont use
                        break;
                    case LinkReferenceDefinitionGroup:
                        // Still links just they werent placed inside brackets
                        break;
                    default:
                        break;
                }
            }
            return quiz;
        }

        static void ParseImages(ParagraphBlock paragraphBlock, Question currentQuestion, string rootDir)
        {
            if (paragraphBlock.Inline == null)
                return;
            foreach (var descendant in paragraphBlock.Inline.Descendants())
            {
                if (descendant is LinkInline linkInline)
                {
                    if (linkInline.IsImage)
                    {
                        currentQuestion.ImagePath = rootDir + @"\" + linkInline.Url?.Replace("/", @"\").Replace("?raw=true", "").Replace("?raw=png", "");
                    }
                }
            }
        }

        // Parse heading to question, if it is not a question (4 level heading) then returns null
        static Question? ParseQuestion(HeadingBlock headingBlock, List<Question> quiz)
        {
            if (headingBlock.Level == 4)
            {
                if (headingBlock.Inline is null)
                    return null;
                string? question = null;
                foreach (var descendant in headingBlock.Inline.Descendants())
                {
                    if (descendant is LiteralInline)
                        question += descendant.ToString();
                    else if (descendant is CodeInline descendantCode)
                        question += descendantCode.Content.ToString();
                }
                quiz.Add(new Question(question ?? throw new Exception("There is no question.")));
            }
            return null;
        }

        // Parse listblock to answer(s) and add it to the question
        static void ParseAnswer(ListBlock listBlock, Question currentQuestion)
        {
            foreach (ListItemBlock listItemBlock in listBlock.Cast<ListItemBlock>())
            {
                if (listItemBlock.LastChild is ParagraphBlock paragraphBlock)
                {
                    if (paragraphBlock.Inline?.FirstChild is Markdig.Extensions.TaskLists.TaskList taskList)
                    {
                        string answer = "";
                        foreach (var descendant in paragraphBlock.Inline.Descendants())
                        {
                            if (descendant is LiteralInline && descendant.ToString() != " :")
                                answer += descendant.ToString();
                            else if (descendant is CodeInline descendantCode)
                                answer += descendantCode.Content.ToString();
                        }
                        currentQuestion.Answers.Add(new Answer(answer, taskList.Checked));
                    }
                }
            }
        }

        static void ParseCodeSnippet(FencedCodeBlock codeBlock, Question currentQuestion, bool isReadingQuestion)
        {
            string? language = codeBlock.Info;
            string? code = codeBlock.Lines.ToString();
            if (language != null && code != null)
            {
                if (isReadingQuestion)
                {
                    currentQuestion.CodeBlock ??= new CodeSnippet(language, code);
                }
                else
                {
                    currentQuestion.Answers[^1].CodeBlock ??= new CodeSnippet(language, code);
                }
            }
        }
    }
}
