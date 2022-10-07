namespace BattleshipsApi.Entities;

public class Board
{
    public Cell[][] Cells { get; set; }

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
    }
}