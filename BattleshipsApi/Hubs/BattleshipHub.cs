using AutoMapper;
using BattleshipsApi.Entities;
using BattleshipsApi.Handlers;
using BattleshipsApi.Helpers;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace BattleshipsApi.Hubs;

[SignalRHub]
public class BattleshipHub : Hub
{
    private readonly QueueHandler _queueHandler;
    private readonly GameLogicHandler _gameLogicHandler;

    public BattleshipHub(QueueHandler queueHandler, IMapper mapper, GameLogicHandler gameLogicHandler)
    {
        _queueHandler = queueHandler;
        _gameLogicHandler = gameLogicHandler;
    }
    
    public async Task JoinQueue(string name)
    {
        var player = new Player(Context.ConnectionId, name);
        var moreThanTwoPlayersInTheQueue = _queueHandler.AddPlayerToQueue(player);

        if (moreThanTwoPlayersInTheQueue)
        {
            var players = _queueHandler.ReturnLastTwoPlayers();
            StartGame(players.Item1, players.Item2);
        }
    }

    public async Task PlaceShips(List<Ship> ships)
    {
        if (ships.Count != 5 || ships.Select(x => x.Type).Distinct().Count() != ships.Count)
        {
            Console.WriteLine("Invalid battleship configuration");
            return;
        }
        
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);

        if (session.IsGameOver)
        {
            Console.WriteLine("Game is over bro");
            return;
        }
        
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        if (player.PlacedShips || session.AreShipsPlaced)
        {
            Console.WriteLine("Ships already placed");
            return;
        }
        
        var board = player.Board;
        
        try
        {
            _gameLogicHandler.PlaceShipsToBoard(ships, board);
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }

        session.GetPlayerByConnectionId(Context.ConnectionId).PlacedShips = true;
        
        if (session.AllPlayersPlacedShips)
        {
            session.AreShipsPlaced = true;
            session.NextPlayerTurnConnectionId = session.PlayerOne.ConnectionId;
        }
        
        SendGameData(session);
    }

    public async Task MakeMove(Move move)
    {
        //TODO: validation
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);
        
        if (session.IsGameOver)
        {
            Console.WriteLine("Game is over bro");
            return;
        }

        if (session.NextPlayerTurnConnectionId != Context.ConnectionId)
        {
            Console.WriteLine("it's not your move bro");
            return;
        }

        var player = session.GetEnemyPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;

        bool hasShipBeenHit;
        bool hasShipBeenDestroyed;

        try
        {
            (hasShipBeenHit, hasShipBeenDestroyed) = _gameLogicHandler.MakeMoveToEnemyBoard(move, board);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        
        if (!hasShipBeenHit)
        {
            session.SetMoveToNextPlayer();
        }

        if (hasShipBeenDestroyed)
        {
            player.DestroyedShipCount++;
            if (session.Settings.ShipCount >= player.DestroyedShipCount)
            {
                session.IsGameOver = true;
                session.WinnerConnectionId = Context.ConnectionId;
            }
        }
        
        SendGameData(session);
    }

    public async Task RequestData()
    {
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);
        SendGameData(session);
    }
    
    public async void StartGame(Player player1, Player player2)
    {
        var session = SessionHelpers.CreateSession(player1, player2);
        await Clients.Clients(player1.ConnectionId, player2.ConnectionId).SendAsync("startGame");
        SendGameData(session);
    }
    
    public async void SendGameData(GameSession gameSession)
    {
        var gameDataPlayerOne = _gameLogicHandler.MapSessionToGameDataDtoPlayerOne(gameSession);
        var gameDataPlayerTwo = _gameLogicHandler.MapSessionToGameDataDtoPlayerTwo(gameSession);

        await Clients.Client(gameSession.PlayerOne.ConnectionId).SendAsync("gameData", gameDataPlayerOne);
        await Clients.Client(gameSession.PlayerTwo.ConnectionId).SendAsync("gameData", gameDataPlayerTwo);
    }
}

