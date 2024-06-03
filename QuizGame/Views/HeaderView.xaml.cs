namespace QuizGame.Views;

public partial class HeaderView : ContentView
{
	public HeaderView()
	{
		InitializeComponent();
		BindingContext = this;
	}

	public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? ImagePath { get; set; }
}