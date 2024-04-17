using MathOrWords;

namespace UnitTestMathOrWords;

public class UnitTestWordsGame
{
	[Fact]
	public void TestCheckConstraint()
	{
		WordsGamePage page = new WordsGamePage();

		char letter = 'A';
		string[] answers = { "Math", "Nothing", "Armor", "Schema" };

		for (int i = 0; i < answers.Length; i++)
		{
			string answer = answers[i].ToLower();
			bool answerValid = page.CheckConstraint(answer, letter, i);
			Assert.True(answerValid);
		}	
	}
}