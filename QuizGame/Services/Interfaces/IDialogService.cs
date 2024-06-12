using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Services.Interfaces
{
    public interface IDialogService
    {
        Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons);
        Task<bool> DisplayAlertAsync(string title, string text, string accept, string cancel);
    }
}
