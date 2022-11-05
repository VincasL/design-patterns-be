﻿using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Facades;
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

    public async Task JoinQueue(string name,string nation)
    {
        var player = new Player(Context.ConnectionId, name);
        if (nation == "Russian")
        {
            player.nationType = NationType.Russian;
        }
        else if (nation == "American")
        {
            player.nationType = NationType.American;
        }
        else if (nation == "German")
        {
            player.nationType = NationType.German;
        }
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

        if (player.PlacedShips.Count != session.Settings.ShipCount ||
            player.PlacedMines.Count != session.Settings.MineCount) 
        {
            throw new Exception("Not all units placed");
        }

        player.AreAllUnitsPlaced = true;

        SendGameData(session);
    }

    public async Task PlaceShip(CellCoordinates cellCoordinates, ShipType type)
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var board = player.Board;

        var factory = _battleshipsFacade.Factory;
        var ship = factory.CreateShip(type, player.nationType);

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
    
    public async Task PlaceMine(CellCoordinates cellCoordinates, MineType type)
    {
        var factory = _battleshipsFacade.Factory;

        var mine = factory.CreateMine(type, NationType.American);
        
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var player = session.GetPlayerByConnectionId(Context.ConnectionId);
        var enemyPlayer = session.GetEnemyPlayerByConnectionId(Context.ConnectionId);
        var enemyBoard = enemyPlayer.Board;
        
        if (player.AreAllUnitsPlaced || session.AllPlayersPlacedUnits)
        {
            throw new Exception("all units are placed");
        }

        if (player.PlacedMines.Count > session.Settings.MineCount)
        {
            throw new Exception("all mines are placed");
        }

        if(player.PlacedMines.Any(placedMine => placedMine.Type == mine.Type))
        {
            throw new Exception("Such ship has already been placed");
        }
        
        _battleshipsFacade.PlaceMineToBoard(mine , enemyBoard, cellCoordinates);
        player.PlacedMines.Add(mine);

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

        var unitCoordinates = new List<CellCoordinates>();

        foreach (var cell in board.Cells)
        {
            if (cell.Ship == ship)
            {
                unitCoordinates.Add(new CellCoordinates { X = cell.X, Y = cell.Y });
            }
        }
        foreach (var cell in unitCoordinates)
        {
            if ((cell.Y + ship.Length > board.BoardSize && !ship.IsHorizontal) || (cell.X + ship.Length > board.BoardSize && ship.IsHorizontal))
            {
                throw new Exception("out of bounds");
            }
        }
        if (!ship.IsHorizontal)
        {
            var first_coord = unitCoordinates[0];
            for (int i = 1; i < ship.Length; i++)
            {
                if (board.Cells[first_coord.X, first_coord.Y + i].Ship != null)
                {
                    throw new Exception("ship already exists below");
                }
            }
            for (int i = 1; i < unitCoordinates.Count; i++)
            {
                board.Cells[unitCoordinates[i].X, unitCoordinates[i].Y].Ship = null;
                board.Cells[first_coord.X, first_coord.Y + i].Ship = ship;
            }
            ship.IsHorizontal = true;
        }
        else
        {
            var first_coord = unitCoordinates[0];
            for (int i = 1; i < ship.Length; i++)
            {
                if (board.Cells[first_coord.X + i, first_coord.Y].Ship != null)
                {
                    throw new Exception("ship already exists to the right");
                }
            }
            for (int i = 1; i < unitCoordinates.Count; i++)
            {
                board.Cells[unitCoordinates[i].X, unitCoordinates[i].Y].Ship = null;
                board.Cells[first_coord.X + i, first_coord.Y].Ship = ship;
            }
            ship.IsHorizontal = false;
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

        var (hasShipBeenHit, hasShipBeenDestroyed) = _battleshipsFacade.MakeMoveToEnemyBoard(cellCoordinates, board);
        
        // moves heat seeking mine
        if (!hasShipBeenDestroyed && !hasShipBeenHit)
        {
            var mine = board.getHeatSeakingMine();
            if (mine != null && mine.HasExploded == false)
            {
                mine.MoveStrategy.MoveDifferently(board, mine);
            }
        }

        if (hasShipBeenDestroyed)
        {
            player.Board.DestroyedShipCount++;
        }
        
        if (!hasShipBeenHit)
        {
            var destroyedShipCountByMines = _battleshipsFacade.ExplodeMinesInCellsIfThereAreShips(board);
            if (destroyedShipCountByMines > 0)
            {
                hasShipBeenDestroyed = true;
                player.Board.DestroyedShipCount += destroyedShipCountByMines;
            }
            session.SetMoveToNextPlayer();
        }

        // check if all ships destroyed
        if (session.Settings.ShipCount <= player.Board.DestroyedShipCount)
        {
            session.IsGameOver = true;
            session.GetPlayerByConnectionId(Context.ConnectionId).Winner = true;
        }
        
        SendGameData(session);
    }
    public async Task MoveUnit(CellCoordinates coordinates, MoveDirection direction, bool isEnemyBoard)
    {
        var session = _battleshipsFacade.GetSessionByConnectionId(Context.ConnectionId);
        var board = isEnemyBoard
            ? session.GetEnemyPlayerByConnectionId(Context.ConnectionId).Board
            : session.GetPlayerByConnectionId(Context.ConnectionId).Board;
        var unit = _battleshipsFacade.GetUnitByCellCoordinates(coordinates, board, isEnemyBoard? typeof(Mine) : typeof(Ship));

        if (unit == null)
        {
            throw new Exception("no unit :(");
        }

        MoveStrategy strategy = direction switch
        {
            MoveDirection.Up => new MoveUp(),
            MoveDirection.Right => new MoveRight(),
            MoveDirection.Down => new MoveDown(),
            MoveDirection.Left => new MoveLeft(),
            _ => new DontMove()
        };

        unit.MoveStrategy = strategy;
        unit.MoveStrategy.MoveDifferently(board, unit);
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
        var playerOneSessionData = ((GameSession)gameSession.Clone()).ShowPlayerOneShips().ShowPlayerTwoMines();
        var playerTwoSessionData = ((GameSession)gameSession.Clone()).SwapPlayers().ShowPlayerOneShips().ShowPlayerTwoMines();

        // var playerOneSessionDataShallowClone = ((GameSession)gameSession.ShallowClone()).ShowPlayerOneShips();


        Console.WriteLine($"main\n{gameSession}\n");
        Console.WriteLine($"deep\n{playerOneSessionData}\n");
        // Console.WriteLine($"shallow\n{playerOneSessionDataShallowClone}\n");

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
