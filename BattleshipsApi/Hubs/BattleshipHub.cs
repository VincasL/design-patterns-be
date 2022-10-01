using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace BattleshipsApi.Hubs;

[SignalRHub]
public class BattleshipHub : Hub
{
    public async Task askServer(string someTextFromClient)
    {
        string tempString;

        if(someTextFromClient == "hey")
        {
            tempString = "message was 'hey'";
        }
        else
        {
            tempString = "message was something else";
        }

        await Clients.Clients(this.Context.ConnectionId).SendAsync("askServerResponse", tempString);
    }

}