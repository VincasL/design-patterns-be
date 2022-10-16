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

    public BattleshipHub(QueueHandler queueHandler, GameLogicHandler gameLogicHandler)
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
            throw new Exception("Ships already placed");
        }
        
        if (session.IsGameOver)
        {
            throw new Exception("Game is over bro");
        }

        if (player.PlacedShips.Count != 5)
        {
            throw new Exception("Not all ships placed");
        }

        player.AreAllShipsPlaced = true;
        
        SendGameData(session);
    }

    public async Task PlaceShip(Ship ship)
    {
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;
        
        if (player.AreAllShipsPlaced || session.AllPlayersPlacedShips)
        {
            throw new Exception("all ships are placed");
        }

        if (player.PlacedShips.Count > 5)
        {
            throw new Exception("enough ships are placed");
        }
        
        if(player.PlacedShips.Any(placedShip => placedShip.Type == ship.Type))
        {
            throw new Exception("Such ship has already been placed");
        }
        
        _gameLogicHandler.PlaceShipToBoard( ship , board);
        player.PlacedShips.Add(ship);

        SendGameData(session);
    }
    
    public async Task UndoPlaceShip(Move move)
    {
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;
        
        if (player.AreAllShipsPlaced || session.AllPlayersPlacedShips)
        {
            throw new Exception("all ships are placed");
        }

        var ship = _gameLogicHandler.GetShipByCellCoordinates(move, board);
        if (ship == null)
        {
            throw new Exception("no ship here");
        }
        
        _gameLogicHandler.UndoPlaceShipToBoardByCell(ship, board);
        var removed = player.PlacedShips.Remove(ship);
        if (!removed)
        {
            throw new Exception("Ship not removed");
        }
        
        SendGameData(session);
    }

    public async Task RotateShip(Move move)
    {
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);
        var board = session.GetPlayerByConnectionId(Context.ConnectionId).Board;
        
        var ship = _gameLogicHandler.GetShipByCellCoordinates(move, board);
        
        if (ship == null)
        {
            throw new Exception("No ship to rotate in this cell");
        }
        
        _gameLogicHandler.UndoPlaceShipToBoardByCell(ship, board);

        try
        {
            ship.IsHorizontal = !ship.IsHorizontal;
            _gameLogicHandler.PlaceShipToBoard(ship, board);

        }
        catch
        {
            // rollback
            ship.IsHorizontal = !ship.IsHorizontal;
            _gameLogicHandler.PlaceShipToBoard(ship, board);
            throw;
        }
            
        SendGameData(session);
    }
    
    public async Task MakeMove(Move move)
    {
        //TODO: validation
        var session = SessionHelpers.GetSessionByConnectionId(Context.ConnectionId);

        if (!session.AllPlayersPlacedShips)
        {
            throw new Exception("Not all ships placed");
        }

        if (session.IsGameOver)
        {
            throw new Exception("Game is over bro");
        }

        if (session.NextPlayerTurnConnectionId != Context.ConnectionId)
        {
            throw new Exception("it's not your move bro");
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
            throw new Exception(e.Message);
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

    public async void SendGameData(GameSession gameSession)
    {

        var gameDataPlayerOne = _gameLogicHandler.MapSessionToGameDataDtoPlayerOne(gameSession);
        var gameDataPlayerTwo = _gameLogicHandler.MapSessionToGameDataDtoPlayerTwo(gameSession);

        await Clients.Client(gameSession.PlayerOne.ConnectionId).SendAsync("gameData", gameDataPlayerOne);
        await Clients.Client(gameSession.PlayerTwo.ConnectionId).SendAsync("gameData", gameDataPlayerTwo);
    }

    public async Task AssignNewConnectionId(string connectionId)
    {
        var session = SessionHelpers.GetSessionByConnectionId(connectionId);
        SessionHelpers.BindNewConnectionIdToPlayer(connectionId, Context.ConnectionId, session);
        SendGameData(session);
    }
}

