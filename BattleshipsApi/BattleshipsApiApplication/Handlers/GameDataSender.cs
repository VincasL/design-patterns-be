using BattleshipsApi.DTO;
using BattleshipsApi.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BattleshipsApi.Handlers;

public class GameDataSender
{
    private readonly IHubContext<BattleshipHub> _context;
    public GameDataSender(IHubContext<BattleshipHub> context)
    {
        _context = context;
    }
    
    public async Task SendGameData(GameData data, string connectionId)
    {
        Console.WriteLine($"Sending data to ${connectionId}");
        await _context.Clients.Client(connectionId).SendAsync("gameData", data);
    }

    public async Task SendStartGame(string playerOneConnectionId, string playerTwoConnectionId)
    {
        await _context.Clients.Clients(playerOneConnectionId, playerTwoConnectionId).SendAsync("startGame");
    }
}