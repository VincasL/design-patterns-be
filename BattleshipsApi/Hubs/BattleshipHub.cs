using BattleshipsApi.DTO;
using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Handlers;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace BattleshipsApi.Hubs;

[SignalRHub]
public class BattleshipHub : Hub
{
    private QueueHandler _queueHandler;
    private SessionsHandler _sessionsHandler;

    public BattleshipHub(QueueHandler queueHandler, SessionsHandler sessionsHandler)
    {
        _queueHandler = queueHandler;
        _sessionsHandler = sessionsHandler;
    }
    
    public async Task JoinQueue(string name)
    {
        var player = new GamePlayer(Context.ConnectionId, name);
        var moreThanTwoPlayersInTheQueue = _queueHandler.AddPlayerToQueue(player);

        if (!moreThanTwoPlayersInTheQueue)
        {
            return;
        }
        
        var players = _queueHandler.ReturnLastTwoPlayers();
        StartGame(players.Item1, players.Item2);
    }

    public async Task PlaceShips(List<GameShip> ships)
    {
        if (ships.Count != 5 || ships.Select(x => x.Type).Distinct().Count() != ships.Count)
        {
            throw new Exception("Invalid battleship configuration");
        }
        
        var session = _sessionsHandler.GetSessionByConnectionId(Context.ConnectionId);
        session.GetPlayerByConnectionId(Context.ConnectionId).Ships = ships;
        
        if (session.AllPlayersPlacedShips)
        {
            session.AreShipsPlaced = true;
            session.PlayerTurn = PlayerTurn.FirstPlayer;
        }
        
        SendGameData(session);
    }

    public async void StartGame(GamePlayer player1, GamePlayer player2)
    {
        var session = _sessionsHandler.CreateSession(player1, player2);
        await Clients.Clients(player1.ConnectionId, player2.ConnectionId).SendAsync("startGame");
        SendGameData(session);
    }
    
    public async void SendGameData(GameSession session)
    {
        await Clients.Clients(session.PlayerOne.ConnectionId, session.PlayerTwo.ConnectionId).SendAsync("gameData", session);
    }
}

