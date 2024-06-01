using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Helpers
{
    public abstract class BaseModelInitializer
    {
        public static async Task<Stream> OpenStreamAsync(string path) => await FileSystem.OpenAppPackageFileAsync(path);

        public static async Task<string> LoadFileAsync(string path)
        {
            using var stream = await BaseModelInitializer.OpenStreamAsync(path);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}
