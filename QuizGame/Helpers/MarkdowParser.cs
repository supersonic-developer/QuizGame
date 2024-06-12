using Markdig;
using Markdig.Extensions.TaskLists;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using QuizGame.Models;
using System;
using System.Security.Policy;

namespace QuizGame.Helpers
{
    public class MarkdowParser(string directory)
    {
        enum ParserState
        {
           ReadingQuestion,
           ReadingAnswer,
           ReadingReference,
           LinkWasRead,
        }

        ParserState state;
        readonly List<Question> quiz = [];
        readonly string directory = directory;
        string last_url = "";
        bool isAnswerRead = false;

        // Parser functions
        public List<Question> ParseQuestions(string mdText)
        {
            // Parse the text 
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            var document = Markdown.Parse(mdText, pipeline);

            // Create the list for questions
            quiz.Clear();

            // Traverse the AST and process the content
            foreach (var block in document)
            {
                switch (block)
                {
                    case HeadingBlock:
                        // Parse heading to question
                        state = ParserState.ReadingQuestion;
                        ParseQuestion((HeadingBlock)block);
                        break;
                    case ListBlock:
                        // Parse list block to answers
                        state = ParserState.ReadingAnswer;
                        ParseListBlock((ListBlock)block);
                        break;
                    case ParagraphBlock:
                        // Parse paragraph block to reference
                        state = ParserState.ReadingReference;
                        ParseParagraphBlock((ParagraphBlock)block);
                        break;
                    case FencedCodeBlock:
                        // Parse code block and add to question or answer
                        ParseCodeSnippet((FencedCodeBlock)block);
                        break;
                }
            }
            return quiz;
        }

        void ParseLiteralInline(LiteralInline literalInline)
        {
            switch (state)
            {
                case ParserState.ReadingAnswer:
                    quiz[^1].Answers[^1].Text += literalInline.ToString();
                    break;
                case ParserState.ReadingReference:
                    quiz[^1].Reference!.Text += literalInline.ToString();
                    break;
                case ParserState.ReadingQuestion:
                    quiz[^1].Text += literalInline.ToString();
                    break;
                case ParserState.LinkWasRead:
                    state = ParserState.ReadingReference;
                    quiz[^1].Reference!.Links.Add((last_url, literalInline.ToString()));
                    break;
            }
        }

        void ParseLinkInline(LinkInline linkInline)
        {
            if (linkInline.IsImage)
            {
                if (state == ParserState.ReadingQuestion)
                {
                    quiz[^1].ImagePath = directory + @"/" + linkInline.Url?.Replace(@"\?raw=[^\.]*\.", ".");
                }
                else if (state == ParserState.ReadingAnswer)
                {
                    quiz[^1].Answers[^1].ImagePath = directory + @"/" + linkInline.Url?.Replace(@"\?raw=[^\.]*\.", ".");
                }
            }
            else if (linkInline.Url != null)
            {
                last_url = linkInline.Url;
                state = ParserState.LinkWasRead;
            }
        }

        void ParseCodeInline(CodeInline codeInline, bool isInlineCode)
        {
            // Only content of paragraph block is considered code snippet
            if (isInlineCode)
            {
                switch (state)
                {
                    case ParserState.ReadingQuestion:
                        quiz[^1].CodeBlock ??= new CodeSnippet("", codeInline.Content.ToString());
                        break;
                    case ParserState.ReadingAnswer:
                        quiz[^1].Answers[^1].CodeBlock ??= new CodeSnippet("", codeInline.Content.ToString());
                        break;
                    case ParserState.ReadingReference:
                        quiz[^1].Reference!.CodeBlock ??= new CodeSnippet("", codeInline.Content.ToString());
                        break;
                }
                
            } // Part of text, not inline code
            else
            {
                switch (state)
                {
                    case ParserState.ReadingQuestion:
                        quiz[^1].Text += codeInline.Content.ToString();
                        break;
                    case ParserState.ReadingAnswer:
                        quiz[^1].Answers[^1].Text += codeInline.Content.ToString();
                        break;
                    case ParserState.ReadingReference:
                        quiz[^1].Reference!.Text += codeInline.Content.ToString();
                        break;
                }
            }
        }

        void ParseParagraphBlock(ParagraphBlock paragraphBlock)
        {
            if (paragraphBlock.Inline == null)
                return;
            if (state == ParserState.ReadingReference)
                quiz[^1].Reference = new Reference([]);
            foreach (var descendant in paragraphBlock.Inline.Descendants())
            {
                switch (descendant)
                {
                    case LiteralInline literalInline:
                        ParseLiteralInline(literalInline);
                        break;
                    case LinkInline linkInline:
                        ParseLinkInline(linkInline);
                        break;
                    case CodeInline codeInline:
                        bool isText = paragraphBlock.Inline.Descendants().Any(d => d is LiteralInline literal && literal.ToString() != " ");
                        ParseCodeInline(codeInline, !isText);
                        break;
                    case TaskList taskList:
                        quiz[^1].Answers[^1].IsCorrect = taskList.Checked;
                        break;
                    default:
                        break;
                }
            }
            isAnswerRead = false;
        }

        // Parse heading to question, if it is not a question (4 level heading) then returns null
        void ParseQuestion(HeadingBlock headingBlock)
        {
            if (headingBlock.Inline is null || headingBlock.Level != 4)
                return;
            quiz.Add(new Question("", []));
            foreach (var descendant in headingBlock.Inline!.Descendants())
            {
                if (descendant is LiteralInline literalInline)
                    ParseLiteralInline(literalInline);
                else if (descendant is CodeInline codeInline)
                   ParseCodeInline(codeInline, false);
            }
            isAnswerRead = false;
        }

        // Parse listblock to answer(s) and add it to the question
        void ParseAnswer(ListBlock listBlock)
        {
            // Iterate list block
            foreach (ListItemBlock listItemBlock in listBlock.Cast<ListItemBlock>())
            {
                // Iterate list item
                foreach (var listItem in listItemBlock.Descendants())
                {
                    switch (listItem)
                    {
                        case ParagraphBlock paragraphBlock:
                            quiz[^1].Answers.Add(new Answer("", false));
                            ParseParagraphBlock(paragraphBlock);
                            break;
                        case FencedCodeBlock fencedCodeBlock:
                            ParseCodeSnippet(fencedCodeBlock);
                            break;
                    }
                }
            }
        }

        void ParseReferenceList(ListBlock listBlock)
        {
            // Iterate list block
            foreach (ListItemBlock listItemBlock in listBlock.Cast<ListItemBlock>())
            {
                // Iterate list item
                foreach (var listItem in listItemBlock.Descendants())
                {
                    if (listItem is ParagraphBlock paragraphBlock)
                    {
                        ParseParagraphBlock(paragraphBlock);
                    }
                }
            }
        }

        void ParseListBlock(ListBlock listBlock)
        {
            if (isAnswerRead)
            {
                state = ParserState.ReadingReference;
                ParseReferenceList(listBlock);
                isAnswerRead = false;
            }
            else 
            {
                ParseAnswer(listBlock);
                isAnswerRead = true;
            }
        }

        void ParseCodeSnippet(FencedCodeBlock codeBlock)
        {
            string? language = codeBlock.Info;
            string? code = codeBlock.Lines.ToString();
            if (language != null && code != null)
            {
                switch (state)
                {
                    case ParserState.ReadingQuestion:
                        quiz[^1].CodeBlock ??= new CodeSnippet(language, code);
                        break;
                    case ParserState.ReadingAnswer:
                        quiz[^1].Answers[^1].CodeBlock ??= new CodeSnippet(language, code);
                        break;
                    case ParserState.ReadingReference:
                        quiz[^1].Reference!.CodeBlock ??= new CodeSnippet(language, code);
                        break;
                }
            }
        }
    }
}