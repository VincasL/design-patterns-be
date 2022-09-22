using Microsoft.AspNetCore.SignalR;

namespace BattleshipsApi.Hubs;

public class BattleshipHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("boi", user, message);
    }
}