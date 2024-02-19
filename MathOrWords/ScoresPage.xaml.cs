using MathOrWords.Data;
using MathOrWords.Models;
using System.Linq;

namespace MathOrWords;

public partial class ScoresPage : ContentPage
{
	public ScoresPage()
	{
		InitializeComponent();
		BindingContext = this;

		PrintGames();
	}

	private void PrintGames()
	{
		IEnumerable<Game> gamesToPrint = App.GameRepository.GetAllGames().OrderByDescending(x => x.Date);

        //gamesToPrint = MainPage.Games.Where(x => x.GameMode == GameMode.Words).OrderByDescending(x => x.Date);

        GamesList.ItemsSource = gamesToPrint;
    }

	private void OnDeleteButtonChosen(object sender, EventArgs e)
	{
		ImageButton button = (ImageButton)sender;
		App.GameRepository.DeleteGame(Convert.ToInt32(button.BindingContext));
		PrintGames();
	}
}