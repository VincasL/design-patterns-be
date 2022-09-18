using Battleship.Data;
using Battleship.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Battleship_API.Handlers
{
    public class PlayersHandler
    {
        private readonly BattleshipsDbContext _context;

        public PlayersHandler(BattleshipsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            var players = await _context.Players.ToListAsync();
            return players;
        }
    }
}
