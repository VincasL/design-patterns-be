using Microsoft.AspNetCore.SignalR;

namespace BattleshipService.Hubs;

public class BattleshipHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("boi", user, message);
    }
}