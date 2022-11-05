namespace BattleshipsApi.Entities;

public class CellCoordinates
{
    public int X { get; set; }
    public int Y { get; set; }

    public CellCoordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public CellCoordinates()
    {
        
    }
}