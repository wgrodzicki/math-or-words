using SQLite;
using MathOrWords.Models;

namespace MathOrWords.Data;

public class GameRepository
{
    private string _dbPath;
    private SQLiteConnection _connection;

    public GameRepository(string dbPath)
    {
        _dbPath = dbPath;
    }

    public void Init()
    {
        _connection = new SQLiteConnection(_dbPath);
        _connection.CreateTable<Game>(); // Creates a table if one doesn't exist
    }

    public List<Game> GetAllGames()
    {
        Init();
        return _connection.Table<Game>().ToList();
    }

    public void AddGame(Game game)
    {
        _connection = new SQLiteConnection(_dbPath);
        _connection.Insert(game);
    }

    public void DeleteGame(int id)
    {
        _connection = new SQLiteConnection(_dbPath);
        _connection.Delete(new { Id = id });
    }
}
