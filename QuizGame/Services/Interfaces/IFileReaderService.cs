using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Services.Interfaces
{
    public interface IFileReaderService
    {
        public Task<string> ReadFileAsync(string path);
    }
}
