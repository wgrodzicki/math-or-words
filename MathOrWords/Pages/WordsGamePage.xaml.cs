using MathOrWords.Controllers;
using MathOrWords.Models;

namespace MathOrWords;

public partial class WordsGamePage : ContentPage
{
    private const int IncorrectAnswersAllowed = 3;

    private readonly string[] _wordCategories = [
        "animal",
        "plant",
        "food",
        "name",
        "profession",
        "country",
        "city",
        "vehicle",
        "tool",
        "object you see"
    ];

    private readonly string[] _constraintCategories = [
        "contains the letter",
        "does not contain the letter",
        "starts with the letter",
        "ends with the letter"
    ];

    private readonly char[] _vowels = ['a', 'e', 'i', 'o', 'u'];

    private int _wordCategoryIndex;
    private int _constraintCategoryIndex;
    private char _letter;

    private int _score = 0;
    private int _incorrectAnswers = 0;


    public WordsGamePage()
	{
		InitializeComponent();
		BindingContext = this;

        NextQuestionButton.IsEnabled = false;
        IncorrectCounterLabel.Text = $"Attempts: {IncorrectAnswersAllowed - _incorrectAnswers}/{IncorrectAnswersAllowed}";
        ScoreLabel.Text = $"Score: {_score}";
        GenerateQuestion();
    }


    /// <summary>
    /// Generates a new question to answer by the player.
    /// </summary>
    private void GenerateQuestion()
    {
        string questionText;

        Random random = new Random();

        // Select random word category (out of 10 defined)
        _wordCategoryIndex = random.Next(0, _wordCategories.Length);

        // Select random constraint category (out of 4 defined)
        _constraintCategoryIndex = random.Next(0, _constraintCategories.Length);

        // Select random uppercase letter (out of the 26 chars in the alphabet, ASCII code 65-90)
        int asciiCode = random.Next(65, 91);
        _letter = Convert.ToChar(asciiCode);

        // Build question
        string article = _vowels.Contains(_wordCategories[_wordCategoryIndex][0]) ? "An" : "A";
        questionText = $"{article} {_wordCategories[_wordCategoryIndex]} that {_constraintCategories[_constraintCategoryIndex]}: {_letter}";

        QuestionLabel.Text = questionText;
    }


    /// <summary>
    /// Handles answer submission and further processing of the game (continue or call the game over).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void OnAnswerSubmitted(object sender, EventArgs e)
	{
        string answer = AnswerEntry.Text;
        AnswerLabel.IsVisible = true;
		SubmitAnswerButton.IsVisible = false;

		// Initial verification if the answer is exists
		if (String.IsNullOrWhiteSpace(answer))
        {
            AnswerLabel.Text = "Invalid answer";
			SubmitAnswerButton.IsVisible = true;
			return;
        }

        // Initial verification if the answer is alphanumerical
        if (answer.All(x => Char.IsLetter(x) == true || Char.IsWhiteSpace(x) == true))
        {
            answer = answer.Trim().ToLower();
            AnswerEntry.IsEnabled = false;
            AnswerLabel.Text = "Checking answer...";

            Task<bool> validationTask = ValidateAnswer(answer);
            bool answerValid = await validationTask;

            if (answerValid)
            {
                AnswerLabel.Text = "Correct!";
                _score++;
                ScoreLabel.Text = $"Score: {_score}";
                SubmitAnswerButton.IsVisible = false;
            }
            else
            {
                AnswerLabel.Text = "Incorrect";
                _incorrectAnswers++;
                IncorrectCounterLabel.Text = $"Attempts: {IncorrectAnswersAllowed - _incorrectAnswers}/{IncorrectAnswersAllowed}";
                SubmitAnswerButton.IsVisible = false;
            }

            if (_incorrectAnswers < IncorrectAnswersAllowed)
            {
                NextQuestionButton.IsEnabled = true;
            }
            else
            {
                GamePage.GameOver(_score, GameMode.Words, QuestionArea, GameOverLabel, IncorrectCounterLabel, ScoreLabel, ReturnButton, "-");
            }
        }
        else
        {
            AnswerLabel.Text = "Invalid answer";
			SubmitAnswerButton.IsVisible = true;
		}
    }


	/// <summary>
	/// Validates player answer against the current constraint and the Wiktionary.
	/// </summary>
	/// <param name="answer"></param>
	/// <returns></returns>
	private async Task<bool> ValidateAnswer(string answer)
    {
        if (!CheckConstraint(answer, _letter, _constraintCategoryIndex))
            return false;

        // Call the Wiktionary API to check if the answer is a valid word
		Task<bool> wikiCheckTask = WiktionaryController.GetWikiData(answer);
        bool result = await wikiCheckTask;

        return result == true ? true : false;
    }


	/// <summary>
	/// Checks if player answer meets the constraint criteria. The constraints are the following:
    /// 0 - contains the given letter;
    /// 1 - does not contain the given letter;
    /// 2 - starts with the given letter;
    /// 3 - ends with the given letter.
	/// </summary>
	/// <param name="answer"></param>
	/// <param name="letter"></param>
	/// <param name="constraintIndex"></param>
	/// <returns></returns>
	public bool CheckConstraint(string answer, char letter, int constraintIndex)
    {
		string letterLower = letter.ToString().ToLower();

		switch (constraintIndex)
		{
			case 0:
				return answer.Contains(letterLower) ? true : false;
			case 1:
				return answer.Contains(letterLower) ? false : true;
			case 2:
				return answer.StartsWith(letterLower) ? true : false;
			case 3:
				return answer.EndsWith(letterLower) ? true : false;
			default:
				return false;
		}
	}


    /// <summary>
    /// Displays the next question.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnNextQuestionChosen(object sender, EventArgs e)
	{
        AnswerEntry.IsEnabled = true;
        AnswerEntry.Text = "";

        AnswerLabel.IsVisible = false;
        AnswerLabel.Text = "";

        SubmitAnswerButton.IsVisible = true;
        NextQuestionButton.IsEnabled = false;

        GenerateQuestion();
    }


    /// <summary>
    /// Switches back to the main page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnReturnButtonChosen(object sender, EventArgs e)
	{
		GamePage.ReturnToMainPage();
	}
}