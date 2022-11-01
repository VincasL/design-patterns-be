namespace BattleshipsApi.Entities;

public class Settings
{
    public Settings(int boardSize = 10, int shipCount = 5, int mineCount = 3)
    {
        BoardSize = boardSize;
        ShipCount = shipCount;
        MineCount = mineCount;
    }

    public int BoardSize { get; set; }
    public int ShipCount { get; set; }
    public int MineCount { get; set; }
}