namespace MathOrWords;

public partial class MathVariantPage : ContentPage
{
	public MathVariantPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private void OnMathOptionChosen(object sender, EventArgs e)
	{
		Button button = (Button)sender;

        Navigation.PushAsync(new MathGamePage(button.Text));
	}
}