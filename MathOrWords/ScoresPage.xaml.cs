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
		IEnumerable<Game> gamesToPrint = MainPage.Games.OrderByDescending(x => x.Date);

        //gamesToPrint = MainPage.Games.Where(x => x.GameMode == GameMode.Words).OrderByDescending(x => x.Date);

        GamesList.ItemsSource = gamesToPrint;
    }
}