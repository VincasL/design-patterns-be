using BattleshipService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BattleshipService.Data
{
    public class BattleshipsDbContext : DbContext
    {
        public BattleshipsDbContext(DbContextOptions<BattleshipsDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Game> Games { get; set; }

        public virtual DbSet<Game_Mine> Game_Mines { get; set; }

        public virtual DbSet<Game_Ship> Game_Ships { get; set; }

        public virtual DbSet<Mine> Mines { get; set; }

        public virtual DbSet<Move> Moves { get; set; }

        public virtual DbSet<Player> Players { get; set; }

        public virtual DbSet<Settings> Settings { get; set; }

        public virtual DbSet<Ship> Ships { get; set; }

        public virtual DbSet<Tile> Tiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Player>().HasData(new Player
            {
                Id = 1,
                Name = "Vinkas"
            },
            new Player
            {
                Id = 2,
                Name = "Marinis"
            });
            modelBuilder.Entity<Game>().HasData(new Game
            {
                Id = 1,
                TimeStamp = (DateTimeOffset)DateTime.UtcNow,
                IsFinished = false,
                PlayerOneId = 1,
                PlayerTwoId = 2,
                MapSize = 100
            });

            modelBuilder.Entity<Game_Mine>().HasData(new Game_Mine
            {
                Id = 1,
                MineId = 1,
                GameId = 1,
                TileId = 1,
                PlayerId = 1
            },
            new Game_Mine
            {
                Id = 2,
                MineId = 1,
                GameId = 1,
                TileId = 2,
                PlayerId = 2
            });

            modelBuilder.Entity<Game_Ship>().HasData(new Game_Ship
            {
                Id = 1,
                ShipId = 1,
                GameId = 1,
                TileId = 3,
                PlayerId = 1
            },
            new Game_Ship
            {
                Id = 2,
                ShipId = 2,
                GameId = 1,
                TileId = 4,
                PlayerId = 2
            });

            modelBuilder.Entity<Mine>().HasData(new Mine
            {
                Id = 1,
                Type = "Bomb_Mine"
            });

            modelBuilder.Entity<Move>().HasData(new Move
            {
                Id = 1,
                GameId = 1,
                TileId = 1,
                PlayerId = 1,
                TimeStamp = (DateTimeOffset)DateTime.UtcNow,
            });

            modelBuilder.Entity<Settings>().HasData(new Settings
            {
                Id = 1,
                IsDarkModeEnabled = true
            });

            modelBuilder.Entity<Ship>().HasData(new Ship
            {
                Id = 1,
                Type = "Big Ship"
            });

            modelBuilder.Entity<Tile>().HasData(new Tile
            {
                Id = 1,
                XCoordinates = 1,
                YCoordinates = 1,
            }, new Tile
            {
                Id = 2,
                XCoordinates = 2,
                YCoordinates = 1,
            }, new Tile
            {
                Id = 3,
                XCoordinates = 1,
                YCoordinates = 2,
            }, new Tile
            {
                Id = 4,
                XCoordinates = 2,
                YCoordinates = 2,
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
