using CommunityToolkit.Mvvm.ComponentModel;
using QuizGame.Helpers;
using System.Collections.ObjectModel;

namespace QuizGame.Models
{
    public partial class Topics : ObservableObject
    {
        // Properties
        [ObservableProperty]
        List<(string Path, string Name)> topicsData;

        public Topics()
        {
            TopicsData = [];
            _ = new TopicsInitializer(this);
        }
    }
}
