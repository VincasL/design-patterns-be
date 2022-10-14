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

    public async Task PlaceShips()
    {
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;

        if (session.AllPlayersPlacedShips || player.AreAllShipsPlaced)
        {
            Console.WriteLine("Ships already placed");
            return;
        }
        
        if (session.IsGameOver)
        {
            Console.WriteLine("Game is over bro");
            return;
        }

        if (player.PlacedShips.Count != 5)
        {
            Console.WriteLine("Not all ships placed");
            return;
        }

        player.AreAllShipsPlaced = true;
        
        SendGameData(session, !session.AllPlayersPlacedShips);
    }

    public async Task PlaceShip(Ship ship)
    {
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;
        
        if (player.AreAllShipsPlaced || session.AllPlayersPlacedShips)
        {
            Console.WriteLine("all ships are placed");
            return;
        }

        if (player.PlacedShips.Count > 5)
        {
            Console.WriteLine("enough ships are placed");
            return;
        }
        
        if(player.PlacedShips.Any(placedShip => placedShip.Type == ship.Type))
        {
            Console.WriteLine("Such ship has already been placed");
            return;
        }
        
        try
        {
            _gameLogicHandler.PlaceShipToBoard( ship , board);
            player.PlacedShips.Add(ship);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw e;
            return;
        }
        
        SendGameData(session, true);
    }
    
    public async Task UndoPlaceShip(Move move)
    {
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;
        
        if (player.AreAllShipsPlaced || session.AllPlayersPlacedShips)
        {
            Console.WriteLine("all ships are placed");
            return;
        }

        var ship = _gameLogicHandler.GetShipByCellCoordinates(move, board);
        if (ship == null)
        {
            Console.WriteLine("no ship here");
            return;
        }
        
        try
        {
            _gameLogicHandler.UndoPlaceShipToBoardByCell(ship, board);
            var removed = player.PlacedShips.Remove(ship);
            if (!removed)
            {
                throw new Exception("Ship not removed");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return;
        }
        
        SendGameData(session, true);
    }

    public async Task RotateShip(Move move)
    {
        //TODO: implement
    }
    
    public async Task MakeMove(Move move)
    {
        //TODO: validation
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);

        if (!session.AllPlayersPlacedShips)
        {
            Console.WriteLine("Not all ships placed");
            return;
        }

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
            if (session.Settings.ShipCount <= player.DestroyedShipCount)
            {
                session.IsGameOver = true;
                session.GetPlayerByConnectionId(Context.ConnectionId).Winner = true;
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

    public async void SendGameData(GameSession gameSession, bool onlySendToPlayerThatSentTheMessage = false)
    {

        var gameDataPlayerOne = _gameLogicHandler.MapSessionToGameDataDtoPlayerOne(gameSession);
        var gameDataPlayerTwo = _gameLogicHandler.MapSessionToGameDataDtoPlayerTwo(gameSession);

        if (onlySendToPlayerThatSentTheMessage)
        {
            await Clients.Client(Context.ConnectionId)
                .SendAsync("gameData",
                    Context.ConnectionId == gameSession.PlayerOne.ConnectionId ? gameDataPlayerOne : gameDataPlayerTwo);
            return;
        }

        await Clients.Client(gameSession.PlayerOne.ConnectionId).SendAsync("gameData", gameDataPlayerOne);
        await Clients.Client(gameSession.PlayerTwo.ConnectionId).SendAsync("gameData", gameDataPlayerTwo);
    }

    public async Task AssignNewConnectionId(string connectionId)
    {
        var session = SessionHelpers.GetSessionByConnectionId(connectionId);
        SessionHelpers.BindNewConnectionIdToPlayer(connectionId, Context.ConnectionId, session);
        SendGameData(session, true);
    }
}

