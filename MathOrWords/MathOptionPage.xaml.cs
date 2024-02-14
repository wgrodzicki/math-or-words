using System.Timers;

namespace MathOrWords;

public partial class MathOptionPage : ContentPage
{
	public string Option { get; set; }

    private const int IncorrectAnswersAllowed = 3;

    private int firstOperand = 0;
	private int secondOperand = 0;
	private int score = 0;
	private int incorrectAnswers = 0;

	public MathOptionPage(string option)
	{
		InitializeComponent();
		Option = option;
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
            }
			else
			{
                AnswerLabel.Text = "Incorrect";
				incorrectAnswers++;
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
		string? mathOperator = Option switch // New switch notation
		{
			"Addition" => "+", // case => value
			"Subtraction" => "-",
			"Multiplication" => "*",
			"Division" => "/",
			_ => "" // Default
		};

		Random random = new Random();
		
		if (Option == "Division")
		{
			do
			{
				firstOperand = random.Next(1, 100);
				secondOperand = random.Next(1, 100);
			} while (firstOperand < secondOperand || firstOperand % secondOperand != 0);
		}
		else
		{
            firstOperand = random.Next(1, 100);
            secondOperand = random.Next(1, 100);
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
		switch (Option)
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
        AnswerLabel.IsVisible = false;
        NextEquationButton.IsEnabled = false;
        GenerateEquation();
	}

	/// <summary>
	/// Ends the game and displays the game over screen
	/// </summary>
	private void GameOver()
	{
		EquationArea.IsVisible = false;
		GameOverLabel.IsVisible = true;
        GameOverLabel.Text = $"Game over! Your score is: {score}";
		ReturnButton.IsVisible = true;
    }

	private void OnReturnButtonChosen(object sender, EventArgs e)
	{
		Navigation.PushAsync(new MainPage());
	}
}