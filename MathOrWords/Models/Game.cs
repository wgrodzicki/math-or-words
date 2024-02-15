namespace MathOrWords.Models;

public class Game
{
    public DateTime Date { get; set; }
    public int Score { get; set; }
    public GameMode GameMode { get; set; }
    public string? MathVariant { get; set; }
}

public enum GameMode
{
    Math,
    Words
}
