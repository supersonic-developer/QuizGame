using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Helpers
{
    public abstract class BaseModelInitializer : ObservableObject
    {
        protected abstract Task Initialize { get; set; }

        protected static async Task<string> LoadFileAsync(string path)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(path);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}
