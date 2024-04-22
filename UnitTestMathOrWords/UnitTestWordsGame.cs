using MathOrWords;

namespace UnitTestMathOrWords;

public class UnitTestWordsGame
{
    [Fact]
    public void CheckConstraint_ContainsLetter_ReturnsTrue()
    {
        WordsGamePage page = new WordsGamePage();
        char letter = 'A';
        string answer = "Math";
        answer = answer.ToLower();

        bool answerValid = page.CheckConstraint(answer, letter, 0);
        Assert.True(answerValid);
    }

    [Fact]
    public void CheckConstraint_NoLetter_ReturnsTrue()
    {
        WordsGamePage page = new WordsGamePage();
        char letter = 'A';
        string answer = "Something";
        answer = answer.ToLower();

        bool answerValid = page.CheckConstraint(answer, letter, 1);
        Assert.True(answerValid);
    }

    [Fact]
    public void CheckConstraint_StartingLetter_ReturnsTrue()
    {
        WordsGamePage page = new WordsGamePage();
        char letter = 'A';
        string answer = "Alphabet";
        answer = answer.ToLower();

        bool answerValid = page.CheckConstraint(answer, letter, 2);
        Assert.True(answerValid);
    }

    [Fact]
    public void CheckConstraint_EndingLetter_ReturnsTrue()
    {
        WordsGamePage page = new WordsGamePage();
        char letter = 'A';
        string answer = "Pizza";
        answer = answer.ToLower();

        bool answerValid = page.CheckConstraint(answer, letter, 3);
        Assert.True(answerValid);
    }
}