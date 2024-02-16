using MathOrWords.Models;

namespace MathOrWords;

public partial class MathVariantPage : ContentPage
{
	private const int DivisionUpperLimit = 100;
	private const int MultiplicationUpperLimit = 11;
	private const int AdditionSubtractionUpperLimit = 100;
    private const int IncorrectAnswersAllowed = 3;

	private string variant;
    private int firstOperand = 0;
	private int secondOperand = 0;
	private int score = 0;
	private int incorrectAnswers = 0;

	public MathVariantPage(string variant)
	{
		InitializeComponent();
		this.variant = variant;

        BindingContext = this;
		
		NextEquationButton.IsEnabled = false;
		GenerateEquation();
	}

	/// <summary>
	/// Handles answer submission and further processing of the game (continue or call the game over).
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnAnswerSubmitted(object sender, EventArgs e)
	{
		string rawAnswer = AnswerEntry.Text;
		int answer;

        AnswerLabel.IsVisible = true;

        if (int.TryParse(rawAnswer, out answer))
		{
			if (ValidateAnswer(answer))
			{
				
				AnswerLabel.Text = "Correct!";
				score++;
				SubmitAnswerButton.IsEnabled = false;
            }
			else
			{
                AnswerLabel.Text = "Incorrect";
				incorrectAnswers++;
                SubmitAnswerButton.IsEnabled = false;
            }

            if (incorrectAnswers < IncorrectAnswersAllowed)
            {
                NextEquationButton.IsEnabled = true;
            }
            else
            {
                GameOver();
            }
        }
		else
		{
            AnswerLabel.Text = "Invalid answer";
        }
	}

	/// <summary>
	/// Generates a new equation to solve by the player. Makes sure the division results only in whole numbers.
	/// </summary>
	private void GenerateEquation()
	{
		string? mathOperator = variant switch // New switch notation
		{
			"Addition" => "+", // case => value
            "Subtraction" => "-",
            "Multiplication" => "*",
            "Division" => "/",
			_ => "" // Default
		};

		Random random = new Random();
		
		if (variant == "Division")
		{
			do
			{
				firstOperand = random.Next(1, DivisionUpperLimit);
				secondOperand = random.Next(1, DivisionUpperLimit);
			} while (firstOperand < secondOperand || firstOperand % secondOperand != 0);
		}
		else if (variant == "Multiplication")

        {
            firstOperand = random.Next(1, MultiplicationUpperLimit);
            secondOperand = random.Next(1, MultiplicationUpperLimit);
        }
		else
		{
            firstOperand = random.Next(1, AdditionSubtractionUpperLimit);
            secondOperand = random.Next(1, AdditionSubtractionUpperLimit);
        }

		EquationLabel.Text = $"{firstOperand} {mathOperator} {secondOperand}";
	}

	/// <summary>
	/// Validates player answer depending on the math operation.
	/// </summary>
	/// <param name="answer"></param>
	/// <returns></returns>
	private bool ValidateAnswer(int answer)
	{
		switch (variant)
		{
			case "Addition":
				return firstOperand + secondOperand == answer ? true : false;
			case "Subtraction":
                return firstOperand - secondOperand == answer ? true : false;
			case "Multiplication":
                return firstOperand * secondOperand == answer ? true : false;
			case "Division":
                return firstOperand / secondOperand == answer ? true : false;
			default:
				return false;
		}
	}

	/// <summary>
	/// Displays the next equation.
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private void OnNextEquationChosen(object sender, EventArgs e)
    {
		AnswerEntry.Text = "";
		AnswerLabel.Text = "";
        SubmitAnswerButton.IsEnabled = true;
        AnswerLabel.IsVisible = false;
        NextEquationButton.IsEnabled = false;
        GenerateEquation();
	}

	/// <summary>
	/// Ends the game, saves the score and displays the game over screen
	/// </summary>
	private void GameOver()
	{
		MainPage.Games.Add(new Models.Game
		{
			Score = score,
			Date = DateTime.Now,
			GameMode = GameMode.Math,
			MathVariant = variant
		});

		EquationArea.IsVisible = false;
		GameOverLabel.IsVisible = true;
        GameOverLabel.Text = $"Game over! Your score is: {score}";
		ReturnButton.IsVisible = true;
    }

	private void OnReturnButtonChosen(object sender, EventArgs e)
	{
		//Navigation.PushAsync(new MainPage());
		ReturnToMainPage();
	}

    private async Task ReturnToMainPage()
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}