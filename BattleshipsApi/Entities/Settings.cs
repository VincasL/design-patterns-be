namespace BattleshipsApi.Entities;

public class Settings
{
    public Settings(int boardSize = 10, int shipCount = 5)
    {
        BoardSize = boardSize;
        ShipCount = shipCount;
    }

    public int BoardSize { get; set; }
    public int ShipCount { get; set; }
}