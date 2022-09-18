using BattleshipService.Data.Models;
using BattleshipService.Data;
using Microsoft.EntityFrameworkCore;

namespace BattleshipService.Handlers
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
