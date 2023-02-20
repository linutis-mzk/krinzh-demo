using Microsoft.EntityFrameworkCore;
using TwitchAPI.Models;
using Krinzh.Models;


namespace Krinzh.Contexts;

public class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options): base(options) {}
    
    public DbSet<MActivity> Activities { get; set; }
    public DbSet<tblTokens> TwitchAccessTokens { get; set; }
    public DbSet<MParameter> Parameters { get; set; }
    public DbSet<tblStreamers> Streamers { get; set; }
    public DbSet<MStreamerStatus> StreamerStatus { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MStreamerStatus>().HasNoKey();
    }
}