namespace MathOrWords;

public partial class MathGamePage : ContentPage
{
	public MathGamePage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private void OnMathOptionChosen(object sender, EventArgs e)
	{
		Button button = (Button)sender;

        Navigation.PushAsync(new MathVariantPage(button.Text));
	}
}