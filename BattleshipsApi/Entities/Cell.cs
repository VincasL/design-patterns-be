using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public class Cell
{
    public int X { get; set; }
    public int Y { get; set; }
    public CellType Type { get; set; }
    public Ship? Ship { get; set; }
    public Mine? Mine { get; set; }


    public Cell(int x, int y)
    {
        Type = CellType.NotShot;
        X = x;
        Y = y;
    }

    public Cell(int x, int y, CellType type, Ship? ship, Mine? mine)
    {
        X = x;
        Y = y;
        Type = type;
        Ship = ship;
        Mine = mine;
    }
}

