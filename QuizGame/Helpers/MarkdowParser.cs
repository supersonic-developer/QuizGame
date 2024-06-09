using Markdig;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using QuizGame.Models;

namespace QuizGame.Helpers
{
    public static class MarkdowParser
    {
        // Parser functions
        public static List<Question> ParseQuestions(string mdText, string directory)
        {
            // Parse the text 
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var document = Markdown.Parse(mdText, pipeline);

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
                        ParseQuestion((HeadingBlock)block, quiz);
                        break;
                    case ListBlock:
                        isReadingQuestion = false;
                        // Add answer(s) to question
                        ParseAnswer((ListBlock)block, quiz[^1], directory);
                        break;
                    case FencedCodeBlock:
                        // Parse code block and add to question or answer
                        ParseCodeSnippet((FencedCodeBlock)block, quiz[^1], isReadingQuestion);
                        break;
                    case ParagraphBlock:
                        // Pictures that is required to use
                        ParseImages((ParagraphBlock)block, quiz[^1], directory, isReadingQuestion);
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

        static void ParseImages(ParagraphBlock paragraphBlock, Question currentQuestion, string rootDir, bool isReadingQuestion)
        {
            if (paragraphBlock.Inline == null)
                return;
            foreach (var descendant in paragraphBlock.Inline.Descendants())
            {
                if (descendant is LinkInline linkInline)
                {
                    if (linkInline.IsImage)
                    {
                        currentQuestion.ImagePath = rootDir + @"/" + linkInline.Url?.Replace(@"\?raw=[^\.]*\.", ".");
                    }
                    else if (linkInline.Url != null)
                    { }
                }
                else if (descendant is CodeInline descendantCode)
                {
                    if (isReadingQuestion)
                    {
                        currentQuestion.CodeBlock ??= new CodeSnippet("", descendantCode.Content.ToString());
                    }
                    else
                    {
                        currentQuestion.Answers[^1].CodeBlock ??= new CodeSnippet("", descendantCode.Content.ToString());
                    }
                }
                else if (descendant is LiteralInline)
                {
                    // Reference to answer
                }
            }
        }

        // Parse heading to question, if it is not a question (4 level heading) then returns null
        static void ParseQuestion(HeadingBlock headingBlock, List<Question> quiz)
        {
            if (headingBlock.Level == 4)
            {
                if (headingBlock.Inline is null)
                    return;
                string? question = null;
                foreach (var descendant in headingBlock.Inline.Descendants())
                {
                    if (descendant is LiteralInline)
                        question += descendant.ToString();
                    else if (descendant is CodeInline descendantCode)
                        question += descendantCode.Content.ToString();
                }
                quiz.Add(new Question(question ?? throw new Exception("There is no question."), []));
            }
        }

        // Parse listblock to answer(s) and add it to the question
        static void ParseAnswer(ListBlock listBlock, Question currentQuestion, string rootDir)
        {
            foreach (ListItemBlock listItemBlock in listBlock.Cast<ListItemBlock>())
            {
                foreach (var listItem in listItemBlock.Descendants())
                {
                    if (listItem is ParagraphBlock paragraphBlock)
                    {
                        if (paragraphBlock.Inline?.FirstChild is Markdig.Extensions.TaskLists.TaskList taskList)
                        {
                            string answer = "";
                            string? imagePath = null;
                            foreach (var descendant in paragraphBlock.Inline.Descendants())
                            {
                                if (descendant is LiteralInline && descendant.ToString() != " :")
                                    answer += descendant.ToString();
                                else if (descendant is CodeInline descendantCode)
                                    answer += descendantCode.Content.ToString();
                                else if (descendant is LinkInline linkInline)
                                {
                                    if (linkInline.IsImage)
                                        imagePath = rootDir + @"/" + linkInline.Url?.Replace(@"\?raw=[^\.]*\.", ".");
                                }
                            }
                            currentQuestion.Answers.Add(new Answer(answer, taskList.Checked, null, imagePath));
                        }
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