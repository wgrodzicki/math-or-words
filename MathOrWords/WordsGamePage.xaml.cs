using MathOrWords.Models;

namespace MathOrWords;

public partial class WordsGamePage : ContentPage
{
    private const int IncorrectAnswersAllowed = 3;

    private readonly string[] wordCategories = [
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

    private readonly string[] constraintCategories = [
        "contains the letter",
        "does not contain the letter",
        "starts with the letter",
        "ends with the letter"
    ];

    private readonly char[] vowels = ['a', 'e', 'i', 'o', 'u'];

    private int wordCategoryIndex;
    private int constraintCategoryIndex;
    private char letter;

    private int score = 0;
    private int incorrectAnswers = 0;


    public WordsGamePage()
	{
		InitializeComponent();
		BindingContext = this;

        NextQuestionButton.IsEnabled = false;
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
        wordCategoryIndex = random.Next(0, wordCategories.Length);

        // Select random constraint category (out of 4 defined)
        constraintCategoryIndex = random.Next(0, constraintCategories.Length);

        // Select random uppercase letter (out of the 26 chars in the alphabet, ASCII code 65-90)
        int asciiCode = random.Next(65, 91);
        letter = Convert.ToChar(asciiCode);

        // Build question
        string article = vowels.Contains(wordCategories[wordCategoryIndex][0]) ? "An" : "A";
        questionText = $"{article} {wordCategories[wordCategoryIndex]} that {constraintCategories[constraintCategoryIndex]}:\n{letter}";

        QuestionLabel.Text = questionText;
    }


    /// <summary>
    /// Handles answer submission and further processing of the game (continue or call the game over).
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnAnswerSubmitted(object sender, EventArgs e)
	{
        string answer = AnswerEntry.Text;
        answer = answer.Trim().ToLower();

        AnswerLabel.IsVisible = true;

        // Initial verification if the answer is alphanumerical
        if (answer.All(x => Char.IsLetter(x) == true))
        {
            AnswerEntry.IsEnabled = false;

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
                NextQuestionButton.IsEnabled = true;
            }
            else
            {
                GamePage.GameOver(score, GameMode.Words, QuestionArea, GameOverLabel, ReturnButton);
            }
        }
        else
        {
            AnswerLabel.Text = "Invalid answer";
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="answer"></param>
    /// <returns></returns>
    private bool ValidateAnswer(string answer)
    {
        string letterLower = letter.ToString().ToLower();

        switch (constraintCategoryIndex)
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

        SubmitAnswerButton.IsEnabled = true;
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