using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public class Board
{
    public Cell[][] Cells { get; set; }
    public int BoardSize { get; }

    public Board(int boardSize)
    {
        var cells = new Cell[boardSize][];
        for(var i = 0; i < boardSize; i++)
        {
            cells[i] = new Cell[boardSize];
        }

        for (var i = 0; i < boardSize; i++)
        {
            for (var j = 0; j < boardSize; j++)
            {
                cells[i][j] = new Cell(i,j);
            }
        }

        Cells = cells;
        BoardSize = boardSize;
    }

    public Board RevealBoardShips()
    {
        foreach (var row in Cells)
        {
            foreach (var cell in row)
            {
                if (cell.Unit != null && cell.Type != CellType.DamagedShip && cell.Type != CellType.DestroyedShip)
                {
                    cell.Type = CellType.Ship;
                }
            }
        }

        return this;
    }
}