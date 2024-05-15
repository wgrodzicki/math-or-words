namespace MathOrWordsAPI.Models;

public class UserModel
{
    public int Id { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public string Email { get; set; } = string.Empty;
}
