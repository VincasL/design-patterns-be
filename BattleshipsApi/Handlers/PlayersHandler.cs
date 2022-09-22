using BattleshipsApi.Models;
using Microsoft.EntityFrameworkCore;


namespace BattleshipsApi.Handlers;

public class PlayersHandler
{
    private readonly ApplicationDbContext _context;

    public PlayersHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Player>> GetAllPlayers()
    {
        var players = await _context.Players.ToListAsync();
        return players;
    }
}