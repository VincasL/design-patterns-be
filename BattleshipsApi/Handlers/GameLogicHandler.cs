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
    
    public GameData MapSessionToGameDataDtoPlayerOne(GameSession gameSession)
    {
        var gameData = _mapper.Map<GameData>(gameSession);
        
        for (var i = 0; i < gameSession.PlayerOne.Board.Cells.Length; i++)
        {
            var row = gameSession.PlayerOne.Board.Cells[i];
            for (var j = 0; j < row.Length; j++)
            {
                var cell = row[j];
                if (cell.Ship != null && cell.Type != CellType.DamagedShip && cell.Type != CellType.DestroyedShip)
                {
                    gameData.PlayerOne.Board.Cells[i][j].Type = CellType.Ship;
                }
            }
        }
        
        gameData.IsYourMove = gameSession.NextPlayerTurnConnectionId == gameSession.PlayerOne.ConnectionId;
        if (gameSession.IsGameOver)
        {
            gameData.Winner = gameSession.PlayerOne.Winner;
        }

        return gameData;
    }
    
    public GameData MapSessionToGameDataDtoPlayerTwo(GameSession gameSession)
    {
        var gameData = _mapper.Map<GameData>(gameSession);
        for (var i = 0; i < gameSession.PlayerTwo.Board.Cells.Length; i++)
        {
            var row = gameSession.PlayerTwo.Board.Cells[i];
            for (var j = 0; j < row.Length; j++)
            {
                var cell = row[j];
                if (cell.Ship != null && cell.Type != CellType.DamagedShip && cell.Type != CellType.DestroyedShip)
                {
                    gameData.PlayerTwo.Board.Cells[i][j].Type = CellType.Ship;
                }
            }
        }
        
        gameData.IsYourMove = gameSession.NextPlayerTurnConnectionId == gameSession.PlayerTwo.ConnectionId;
        if (gameSession.IsGameOver)
        {
            gameData.Winner = gameSession.PlayerTwo.Winner;
        }
        
        // Switch players: so enemy always displays on the right
        (gameData.PlayerOne, gameData.PlayerTwo) = (gameData.PlayerTwo, gameData.PlayerOne);

        return gameData;
    }

    public (bool hasShipBeenHit, bool isGameOver) MakeMoveToEnemyBoard(CellCoordinates cellCoordinates, Board board)
    {
        var hitCell = board.Cells[cellCoordinates.X][ cellCoordinates.Y];

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

        shipHasBeenDestroyed = damagedShipCells.Count == hitCell.Ship!.Length;

        if (shipHasBeenDestroyed)
        {
            foreach (var cell in damagedShipCells)
            {
                cell.Type = CellType.DestroyedShip;
            }
        }

        return (shipHasBeenHit, shipHasBeenDestroyed);
    }

    public void PlaceShipToBoard(Ship ship, Board board, CellCoordinates coordinates)
    {
        if (ship.IsHorizontal)
        {
            for (var y = coordinates.Y; y < coordinates.Y + ship.Length; y++)
            {
                var cell = board.Cells[coordinates.X][y];
                if (cell.Ship != null)
                {
                    // rollback
                    while (y != coordinates.Y)
                    {
                        y--;
                        cell = board.Cells[coordinates.X][y];
                        cell.Ship = null;
                    }
                    
                    throw new Exception("Ships overlap");
                }

                cell.Ship = ship;
            }
        }
        else
        {
            for (var x = coordinates.X; x < coordinates.X + ship.Length; x++)
            {
                var cell = board.Cells[x][coordinates.Y];
                
                if (cell.Ship != null)
                {
                    // rollback
                    while (x != coordinates.X)
                    {
                        x--;
                        cell = board.Cells[x][coordinates.Y];
                        cell.Ship = null;
                    }
                    
                    throw new Exception("Ships overlap");
                }

                cell.Ship = ship;
            }
        }
    }

    public void UndoPlaceShipToBoardByCell(Ship ship, Board board)
    {
        foreach (var row in board.Cells)
        {
            foreach (var cell in row)
            {
                if (cell.Ship == ship)
                {
                    cell.Ship = null;
                }
            }
        }
    }

    public Ship? GetShipByCellCoordinates(CellCoordinates cellCoordinates, Board board)
    {
        var ship = board.Cells[cellCoordinates.X][cellCoordinates.Y].Ship;
        return ship;
    }
}