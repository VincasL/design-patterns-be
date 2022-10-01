using BattleshipsApi.Entities;
using BattleshipsApi.Handlers;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace BattleshipsApi.Hubs;

[SignalRHub]
public class BattleshipHub : Hub
{
    private QueueHandler _queueHandler;

    public BattleshipHub(QueueHandler queueHandler)
    {
        _queueHandler = queueHandler;
    }
    
    public async Task JoinQueue(string name)
    {
        var player = new GamePlayer(Context.ConnectionId, name);
        var moreThanTwoPlayersInTheQueue = _queueHandler.AddPlayerToQueue(player);
        
        if (!moreThanTwoPlayersInTheQueue) return;
        var players = _queueHandler.ReturnLastTwoPlayers();
        StartGame(players.Item1, players.Item2);
    }

    public async void StartGame(GamePlayer player1, GamePlayer player2)
    {
        await Clients.Clients(player1.ConnectionId, player2.ConnectionId).SendAsync("startGame", player1, player2);
    }

}

