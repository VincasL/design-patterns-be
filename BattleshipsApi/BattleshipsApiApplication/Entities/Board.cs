using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using BattleshipsApi.Enums;
using BattleshipsApi.Helpers;

namespace BattleshipsApi.Entities;

public class Board : IPrototype
{
    public Cell[,] Cells { get; set; }
    
    public int BoardSize { get; }
    public int DestroyedShipCount { get; set; }
    
    public Board(Cell[,] cells, int boardSize, int destroyedShipCount)
    {
        Cells = cells;
        BoardSize = boardSize;
        DestroyedShipCount = destroyedShipCount;
    }
    
    public Board(int boardSize)
    {
        var cells = new Cell[boardSize, boardSize];

        for (int i = 0; i < boardSize; i++)
        {
            for (var j = 0; j < boardSize; j++)
            {
                cells[i,j] = new Cell(i, j);
            }
        }

        Cells = cells;
        BoardSize = boardSize;

    }
    public Mine? getHeatSeakingMine()
    {
        Mine? mine = null;
        foreach (var cell in Cells)
        {
            if (cell.Mine != null && cell.Mine.Type == MineType.RemoteControlled)
            {
                mine=cell.Mine;
            }
        }
        return mine;
    }
    public Board RevealBoardShips()
    {
        foreach (var cell in Cells)
        {
            var units = cell.Units;

            if (units.Count != 0)
            {
                Console.WriteLine('a');
            }
            
            if (cell.Ship != null && cell.Type != CellType.DamagedShip && cell.Type != CellType.DestroyedShip)
            {
                cell.Type = CellType.Ship;
            }
        }

        return this;
    }
    
    public Board RevealBoardMines()
    {
        foreach (var cell in Cells)
        {
            if (cell.Mine != null && cell.Type != CellType.DamagedShip && cell.Type != CellType.DestroyedShip)
            {
                cell.Type = CellType.Mine;
            }
        }

        return this;
    }

    public object ShallowClone()
    {
        return this.MemberwiseClone();
    }

    public object Clone()
    {
        var cells = new Cell[BoardSize, BoardSize];

        
        for (var i = 0; i < BoardSize; i++)
        {
            
            for (var j = 0; j < BoardSize; j++)
            {
                var newUnits = new List<Unit>();
                foreach (var unit in Cells[i, j].Units)
                {
                    newUnits.Add(unit.Clone());
                }
                
                cells[i, j] = new Cell(i, j, Cells[i, j].Type, newUnits);
            }
        }

        return new Board(cells, BoardSize, DestroyedShipCount);
    }
}