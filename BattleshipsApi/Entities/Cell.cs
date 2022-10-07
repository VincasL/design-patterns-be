using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public CellType Type { get; set; }
    public Ship? Ship { get; set; }

    public Cell(int x, int y)
    {
        Type = CellType.Empty;
        X = x;
        Y = y;
    }
}

