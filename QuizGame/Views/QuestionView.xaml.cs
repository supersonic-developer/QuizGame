namespace QuizGame.Views;

public partial class QuestionView : ContentView
{
	public QuestionView()
	{
		InitializeComponent();
	}

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		var checkbox = (CheckBox)sender;
		if (e.Value == true)
		{
			foreach (var item in answersCollectionView.ItemsSource)
			{ 
				if(item != checkbox.BindingContext)
			}
		}
    }
}