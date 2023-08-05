using Microsoft.EntityFrameworkCore;

namespace BaseballPlayersAPI.DbModels;

public class BaseballPlayersContext : DbContext
{
    public BaseballPlayersContext(DbContextOptions<BaseballPlayersContext> options) : base(options)
    {
    }
    
    public DbSet<PlayerInfoDb> Players { get; set; }
}