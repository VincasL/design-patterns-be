﻿using AutoMapper;
using BattleshipsApi.DTO;
using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Helpers;

namespace BattleshipsApi.Handlers;

public class GameLogicHandler
{
    private IMapper _mapper;

    public GameLogicHandler(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public GameData MapSessionToGameDataDtoPlayerOne(Session session)
    {
        var gameData = _mapper.Map<GameData>(session);
        
        for (var i = 0; i < session.PlayerOne.Board.Cells.Length; i++)
        {
            var row = session.PlayerOne.Board.Cells[i];
            for (var j = 0; j < row.Length; j++)
            {
                var cell = row[j];
                if (cell.Ship != null && cell.Type != CellType.DamagedShip && cell.Type != CellType.DestroyedShip)
                {
                    gameData.PlayerOne.Board.Cells[i][j].Type = CellType.Ship;
                }
            }
        }
        
        gameData.IsYourMove = session.NextPlayerTurnConnectionId == session.PlayerOne.ConnectionId;
        if (session.IsGameOver)
        {
            gameData.Winner = session.WinnerConnectionId == session.PlayerOne.ConnectionId;
        }

        return gameData;
    }
    
    public GameData MapSessionToGameDataDtoPlayerTwo(Session session)
    {
        var gameData = _mapper.Map<GameData>(session);
        for (var i = 0; i < session.PlayerTwo.Board.Cells.Length; i++)
        {
            var row = session.PlayerTwo.Board.Cells[i];
            for (var j = 0; j < row.Length; j++)
            {
                var cell = row[j];
                if (cell.Ship != null && cell.Type != CellType.DamagedShip && cell.Type != CellType.DestroyedShip)
                {
                    gameData.PlayerTwo.Board.Cells[i][j].Type = CellType.Ship;
                }
            }
        }
        
        gameData.IsYourMove = session.NextPlayerTurnConnectionId == session.PlayerTwo.ConnectionId;
        if (session.IsGameOver)
        {
            gameData.Winner = session.WinnerConnectionId == session.PlayerTwo.ConnectionId;
        }

        return gameData;
    }

    public (bool hasShipBeenHit, bool isGameOver) MakeMoveToEnemyBoard(Move move, Board board)
    {
        var hitCell = board.Cells[move.X][ move.Y];

        if (hitCell.Type != CellType.NotShot)
        {
            throw new Exception("Cell has already been hit");
        }

        var shipHasBeenHit = hitCell.Ship != null;
        var shipHasBeenDestroyed = false;

        if (!shipHasBeenHit)
        {
            hitCell.Type = CellType.Empty;
            return (shipHasBeenHit, shipHasBeenDestroyed);
        }

        hitCell.Type = CellType.DamagedShip;

        var damagedShipCells = new List<Cell>();

        foreach (var row in board.Cells)
        {
            foreach (var cell in row)
            {
                if (cell.Ship == hitCell.Ship && cell.Type == CellType.DamagedShip)
                {
                    damagedShipCells.Add(cell);
                }
            }
        }

        shipHasBeenDestroyed = damagedShipCells.Count == hitCell.Ship!.Type.GetShipLength();

        if (shipHasBeenDestroyed)
        {
            foreach (var cell in damagedShipCells)
            {
                cell.Type = CellType.DestroyedShip;
            }
        }

        return (shipHasBeenHit, shipHasBeenDestroyed);
    }

    public void PlaceShipsToBoard(List<Ship> ships, Board board)
    {
        foreach (var ship in ships)
        {
            if (ship.IsHorizontal)
            {
                for (var y = ship.Cell.Y; y < ship.Cell.Y + ship.Type.GetShipLength(); y++)
                {
                    var cell = board.Cells[ship.Cell.X][y];
                    if (cell.Ship != null)
                    {
                        // consider rollback 
                        throw new Exception("Ships overlap");
                    }
                    cell.Ship = ship;
                }
            }
            else
            {
                for (var x = ship.Cell.X; x < ship.Cell.X + ship.Type.GetShipLength(); x++)
                {
                    var cell = board.Cells[x][ship.Cell.Y];
                    if (cell.Ship != null)
                    {
                        // consider rollback 
                        throw new Exception("Ships overlap");
                    }
                    cell.Ship = ship;
                }
            }
        }
    }
}