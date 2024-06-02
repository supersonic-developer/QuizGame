using Microsoft.VisualStudio.Threading;

namespace QuizGame.Helpers
{ 
    public static class JoinableTaskContextHelper
    {
        private static readonly JoinableTaskContext context = new();

        public static JoinableTaskFactory Factory => context.Factory;
    }
}
