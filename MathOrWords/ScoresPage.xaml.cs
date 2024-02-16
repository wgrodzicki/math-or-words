using MathOrWords.Models;
using System.Linq;

namespace MathOrWords;

public partial class ScoresPage : ContentPage
{
	public ScoresPage()
	{
		InitializeComponent();
		BindingContext = this;

		PrintGames("Math");
	}

	private void PrintGames(string gameMode)
	{
		IEnumerable<Game> gamesToPrint;

        if (gameMode == "Math")
		{
			gamesToPrint = MainPage.Games.Where(x => x.GameMode == GameMode.Math).OrderByDescending(x => x.Date);
		}
		else
		{
            gamesToPrint = MainPage.Games.Where(x => x.GameMode == GameMode.Words).OrderByDescending(x => x.Date);
        }

        GamesList.ItemsSource = gamesToPrint;
    }
}