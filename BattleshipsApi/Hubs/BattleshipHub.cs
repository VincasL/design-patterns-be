using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Facades;
using BattleshipsApi.Factories;
using BattleshipsApi.Strategies;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace BattleshipsApi.Hubs;

[SignalRHub]
public class BattleshipHub : Hub
{
    private readonly BattleshipsFacade _battleshipsFacade;

    public BattleshipHub(BattleshipsFacade battleshipsFacade)
    {
        _battleshipsFacade = battleshipsFacade;
    }

    public async Task JoinQueue(string name)
    {
        var player = new Player(Context.ConnectionId, name);
        var moreThanTwoPlayersInTheQueue = _battleshipsFacade.AddPlayerToQueue(player);

        if (moreThanTwoPlayersInTheQueue)
        {
            var players = _battleshipsFacade.ReturnLastTwoPlayers();
            StartGame(players.Item1, players.Item2);
        }
    }

    public async Task PlaceShips()
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;

        if (session.AllPlayersPlacedUnits || player.AreAllUnitsPlaced)
        {
            throw new Exception("Ships already placed");
        }
        
        if (session.IsGameOver)
        {
            throw new Exception("Game is over bro");
        }

        if (player.PlacedShips.Count != session.Settings.ShipCount)
        {
            throw new Exception("Not all ships placed");
        }

        player.AreAllUnitsPlaced = true;
        
        SendGameData(session);
    }

    public async Task PlaceShip(CellCoordinates cellCoordinates, ShipType type)
    {
        var factory = new AbstractFactory();

        var ship = factory.CreateShip(type,"American");
        //var ship = factory.CreateShip(type, "Russian");
        //var ship = factory.CreateShip(type, "German");

        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;
        
        if (player.AreAllUnitsPlaced || session.AllPlayersPlacedUnits)
        {
            throw new Exception("all ships are placed");
        }

        if (player.PlacedShips.Count > session.Settings.ShipCount)
        {
            throw new Exception("enough ships are placed");
        }
        
        if(player.PlacedShips.Any(placedShip => placedShip.Type == ship.Type))
        {
            throw new Exception("Such ship has already been placed");
        }
        
        _battleshipsFacade.PlaceShipToBoard(ship , board, cellCoordinates);
        player.PlacedShips.Add(ship);

        SendGameData(session);
    }
    
    public async Task UndoPlaceShip(CellCoordinates cellCoordinates)
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;
        
        if (player.AreAllUnitsPlaced || session.AllPlayersPlacedUnits)
        {
            throw new Exception("all ships are placed");
        }

        var ship = _battleshipsFacade.GetUnitByCellCoordinates(cellCoordinates, board) as Ship;
        if (ship == null)
        {
            throw new Exception("no ship here");
        }
        
        _battleshipsFacade.UndoPlaceShipToBoardByCell(ship, board);
        var removed = player.PlacedShips.Remove(ship);
        if (!removed)
        {
            throw new Exception("Ship not removed");
        }
        
        SendGameData(session);
    }

    public async Task RotateShip(CellCoordinates cellCoordinates)
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var board = session.GetPlayerByConnectionId(Context.ConnectionId).Board;
        
        var ship = _battleshipsFacade.GetUnitByCellCoordinates(cellCoordinates, board) as Ship;
        
        if (ship == null)
        {
            throw new Exception("No ship to rotate in this cell");
        }
        
        _battleshipsFacade.UndoPlaceShipToBoardByCell(ship, board);

        try
        {
            ship.IsHorizontal = !ship.IsHorizontal;
            _battleshipsFacade.PlaceShipToBoard(ship, board, cellCoordinates);

        }
        catch
        {
            // rollback
            ship.IsHorizontal = !ship.IsHorizontal;
            _battleshipsFacade.PlaceShipToBoard(ship, board, cellCoordinates);
            throw;
        }
            
        SendGameData(session);
    }
    
    public async Task MakeMove(CellCoordinates cellCoordinates)
    {
        //TODO: validation
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);

        if (!session.AllPlayersPlacedUnits)
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
            (hasShipBeenHit, hasShipBeenDestroyed) = _battleshipsFacade.MakeMoveToEnemyBoard(cellCoordinates, board);
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
    public async Task MoveShipUp(CellCoordinates coordinates)
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var board = session.GetPlayerByConnectionId(Context.ConnectionId).Board;
        var ship = _battleshipsFacade.GetUnitByCellCoordinates(coordinates, board);

        if (ship == null)
        {
            throw new Exception("no ship :(");
        }
        ship.MoveStrategy = new MoveUp();
        ship.MoveStrategy.MoveDifferently(board, ship);
        SendGameData(session);
    }
    public async Task MoveShipDown(CellCoordinates coordinates)
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var board = session.GetPlayerByConnectionId(Context.ConnectionId).Board;
        var ship = _battleshipsFacade.GetUnitByCellCoordinates(coordinates, board);
        if (ship == null)
        {
            throw new Exception("no ship :(");
        }
        ship.MoveStrategy = new MoveDown();
        ship.MoveStrategy.MoveDifferently(board, ship);

        SendGameData(session);
    }
    public async Task MoveShipToTheLeft(CellCoordinates coordinates)
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var board = session.GetPlayerByConnectionId(Context.ConnectionId).Board;
        var ship = _battleshipsFacade.GetUnitByCellCoordinates(coordinates, board);
        if (ship == null)
        {
            throw new Exception("no ship :(");
        }
        ship.MoveStrategy = new MoveLeft();
        ship.MoveStrategy.MoveDifferently(board, ship);

        SendGameData(session);
    }

    public async Task MoveShipToTheRight(CellCoordinates coordinates)
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var board = session.GetPlayerByConnectionId(Context.ConnectionId).Board;
        var ship = _battleshipsFacade.GetUnitByCellCoordinates(coordinates, board);
        if (ship == null)
        {
            throw new Exception("no ship :(");
        }
        ship.MoveStrategy = new MoveRight();
        ship.MoveStrategy.MoveDifferently(board, ship);

        SendGameData(session);
    }

    public async Task RequestData()
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        SendGameData(session);
    }

    public async void StartGame(Player player1, Player player2)
    {
        var session = _battleshipsFacade.CreateSession(player1, player2);
        await _battleshipsFacade.StartGame(session);
        SendGameData(session);
    }

    public async void SendGameData(GameSession gameSession)
    {
        var playerOneSessionData = gameSession.Clone().ShowPlayerOneShips();
        var playerTwoSessionData = gameSession.Clone().SwapPlayers().ShowPlayerOneShips();

        await _battleshipsFacade.SendGameData(playerOneSessionData);
        await _battleshipsFacade.SendGameData(playerTwoSessionData);
    }

    public async Task AssignNewConnectionId(string connectionId)
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(connectionId);
        _battleshipsFacade.BindNewConnectionIdToPlayer(connectionId, Context.ConnectionId, session);
        SendGameData(session);
    }
}

