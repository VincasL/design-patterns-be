using BattleshipsApi.Entities;
using BattleshipsApi.Entities.Mines;
using BattleshipsApi.Entities.Ships;
using BattleshipsApi.Enums;
using BattleshipsApi.Template;

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
        IPlaceItem ships = new Battleship();
        ships.Place(ship,board,coordinates);
    }
    
    public void PlaceMineToBoard(Mine mine, Board board, CellCoordinates cellCoordinates)
    {
        IPlaceItem mines = new HugeMine();
        mines.Place(mine,board, cellCoordinates);
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
            if (cell.Ship != null && cell.Mine != null && !(cell.Type == CellType.DamagedShip) && !(cell.Type == CellType.DestroyedShip)) // 
            {
                // cell.Mine.HasExploded = true;
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