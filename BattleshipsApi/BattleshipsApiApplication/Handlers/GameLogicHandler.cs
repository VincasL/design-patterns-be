using BattleshipsApi.Entities;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Handlers;

public class GameLogicHandler
{
    public (bool hasShipBeenHit, bool hasShipBeenDestroyed) MakeMoveToEnemyBoard(CellCoordinates cellCoordinates, Board board)
    {
        var hitCell = board.Cells[cellCoordinates.X, cellCoordinates.Y];

        if (hitCell.Type != CellType.NotShot)
        {
            throw new Exception("Cell has already been hit");
        }

        if (hitCell.Ship == null)
        {
            hitCell.Type = CellType.Empty;
            return (false, false);
        }
        
        hitCell.Type = CellType.DamagedShip;

        var shipHasBeenDestroyed = DestroyShipsIfAllCellsDamaged(board, hitCell.Ship);

        if (!shipHasBeenDestroyed) return (true, false);

        return (true, true);
    }
    
    public void PlaceShipToBoard(Ship ship, Board board, CellCoordinates coordinates)
    {
        if (ship.IsHorizontal)
        {
            for (var y = coordinates.Y; y < coordinates.Y + ship.Length; y++)
            {
                var cell = board.Cells[coordinates.X,y];
                if (cell.Ship != null)
                {
                    // rollback
                    while (y != coordinates.Y)
                    {
                        y--;
                        cell = board.Cells[coordinates.X,y];
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
                var cell = board.Cells[x,coordinates.Y];
                
                if (cell.Ship != null)
                {
                    // rollback
                    while (x != coordinates.X)
                    {
                        x--;
                        cell = board.Cells[x,coordinates.Y];
                        cell.Ship = null;
                    }
                    
                    throw new Exception("Ships overlap");
                }

                cell.Ship = ship;
            }
        }
    }
    
    public void PlaceMineToBoard(Mine mine, Board board, CellCoordinates cellCoordinates)
    {
        var cellToPlaceMineAt = board.Cells[cellCoordinates.X, cellCoordinates.Y];

        if (cellToPlaceMineAt.Mine != null)
        {
            throw new Exception("Mine already placed here");
        }

        if (mine.Type is MineType.Small or MineType.RemoteControlled)
        {
            cellToPlaceMineAt.Mine = mine;
            return;
        }
        
        // if huge mine: 
        
        if (cellToPlaceMineAt.X + 1 == board.BoardSize || cellToPlaceMineAt.X + 1 == board.BoardSize)
        {
            throw new Exception("overflow");
        }

        var cellsToPlaceHugeMineAt = new List<Cell>
        {
            board.Cells[cellToPlaceMineAt.X, cellToPlaceMineAt.Y],
            board.Cells[cellToPlaceMineAt.X + 1, cellToPlaceMineAt.Y],
            board.Cells[cellToPlaceMineAt.X, cellToPlaceMineAt.Y + 1],
            board.Cells[cellToPlaceMineAt.X + 1, cellToPlaceMineAt.Y + 1]
        };
        
        foreach (var cell in cellsToPlaceHugeMineAt)
        {
            if (cell.Mine != null)
            {
                throw new Exception("space occupied");
            }
        }
        
        foreach (var cell in cellsToPlaceHugeMineAt)
        {
            cell.Mine = mine;
        }

    }

    public void UndoPlaceShipToBoardByCell(Unit unit, Board board)
    {
        foreach (var cell in board.Cells)
        {
            if (cell.Ship == unit)
            {
                cell.Ship = null;
            }
        }
    }

    public Unit? GetUnitByCellCoordinates(CellCoordinates cellCoordinates, Board board, Type? unitType = null)
    {
        var unit = board.Cells[cellCoordinates.X, cellCoordinates.Y].Unit(unitType);
        return unit;
    }

    public bool DestroyShipsIfAllCellsDamaged(Board board, Ship ship)
    {
        // If ship is destroyed, mark its cells as destroyed

        var damagedShipCells = new List<Cell>();
        
        foreach (var cell in board.Cells)
        {
            if (cell.Ship == ship && cell.Type == CellType.DamagedShip)
            {
                damagedShipCells.Add(cell);
            }
        }

        var shipHasBeenDestroyed = damagedShipCells.Count == ship.Length;

        if (shipHasBeenDestroyed)
        {
            foreach (var cell in damagedShipCells)
            {
                cell.Type = CellType.DestroyedShip;
            }
        }
        
        return shipHasBeenDestroyed;
    }
    

    public int ExplodeMinesInCellsIfThereAreShips(Board board)
    {
        var destroyedShipsCount = 0;
        
        foreach (var cell in board.Cells)
        {
            if (cell.Ship != null && cell.Mine != null && !cell.Mine.HasExploded)
            {
                cell.Mine.HasExploded = true;
                cell.Type = CellType.DamagedShip;

                if (DestroyShipsIfAllCellsDamaged(board, cell.Ship))
                {
                    destroyedShipsCount++;
                }
            }
        }

        return destroyedShipsCount;
    }
}