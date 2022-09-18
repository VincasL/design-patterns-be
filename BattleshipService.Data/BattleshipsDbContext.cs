using BattleshipService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BattleshipService.Data
{
    public class BattleshipsDbContext : DbContext
    {
        public BattleshipsDbContext(DbContextOptions<BattleshipsDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Player> Players { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Player>().HasData(new Player
        //    {
        //        Nickname = "Vinkas",
        //        GameId = 1,
        //        Points = 100
        //    });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
