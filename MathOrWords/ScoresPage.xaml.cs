using MathOrWords.Models;
using System.Linq;

namespace MathOrWords;

public partial class ScoresPage : ContentPage
{
    public List<Game> Games { get; set; }

	public ScoresPage(List<Game> games)
	{
		InitializeComponent();
		BindingContext = this;

		Games = games;
	}

	private void PrintGames(string gameMode)
	{
		if (gameMode == "Math")
		{
			var gamesToPrint = Games.Where(x => x.GameMode == GameMode.Math).OrderByDescending(x => x.Date);
		}
		else
		{
            var gamesToPrint = Games.Where(x => x.GameMode == GameMode.Words).OrderByDescending(x => x.Date);
        }
	}
}