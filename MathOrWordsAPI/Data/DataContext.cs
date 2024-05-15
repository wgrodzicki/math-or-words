using MathOrWordsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MathOrWordsAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<UserModel> UserModels { get; set; }
}
