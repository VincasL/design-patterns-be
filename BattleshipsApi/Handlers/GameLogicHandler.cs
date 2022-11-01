using AutoMapper;
using BattleshipsApi.DTO;
using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Helpers;

namespace BattleshipsApi.Handlers;

public class GameLogicHandler
{
    public (bool hasShipBeenHit, bool isGameOver) MakeMoveToEnemyBoard(CellCoordinates cellCoordinates, Board board)
    {
        var hitCell = board.Cells[cellCoordinates.X, cellCoordinates.Y];

        if (hitCell.Type != CellType.NotShot)
        {
            throw new Exception("Cell has already been hit");
        }

        var shipHasBeenHit = hitCell.Unit != null;
        var shipHasBeenDestroyed = false;

        if (!shipHasBeenHit)
        {
            hitCell.Type = CellType.Empty;
            return (shipHasBeenHit, shipHasBeenDestroyed);
        }

        hitCell.Type = CellType.DamagedShip;

        var damagedShipCells = new List<Cell>();

        foreach (var cell in board.Cells)
        {
            if (cell.Unit == hitCell.Unit && cell.Type == CellType.DamagedShip)
            {
                damagedShipCells.Add(cell);
            }
        }

        shipHasBeenDestroyed = damagedShipCells.Count == hitCell.Unit!.Length;

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
                var cell = board.Cells[coordinates.X,y];
                if (cell.Unit != null)
                {
                    // rollback
                    while (y != coordinates.Y)
                    {
                        y--;
                        cell = board.Cells[coordinates.X,y];
                        cell.Unit = null;
                    }
                    
                    throw new Exception("Ships overlap");
                }

                cell.Unit = ship;
            }
        }
        else
        {
            for (var x = coordinates.X; x < coordinates.X + ship.Length; x++)
            {
                var cell = board.Cells[x,coordinates.Y];
                
                if (cell.Unit != null)
                {
                    // rollback
                    while (x != coordinates.X)
                    {
                        x--;
                        cell = board.Cells[x,coordinates.Y];
                        cell.Unit = null;
                    }
                    
                    throw new Exception("Ships overlap");
                }

                cell.Unit = ship;
            }
        }
    }

    public void UndoPlaceShipToBoardByCell(Unit unit, Board board)
    {
        foreach (var cell in board.Cells)
        {
            if (cell.Unit == unit)
            {
                cell.Unit = null;
            }
        }
    }

    public Unit? GetUnitByCellCoordinates(CellCoordinates cellCoordinates, Board board)
    {
        var unit = board.Cells[cellCoordinates.X,cellCoordinates.Y].Unit;
        return unit;
    }
}