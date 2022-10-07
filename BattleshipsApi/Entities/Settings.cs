namespace BattleshipsApi.Entities;

public class Settings
{
    public Settings(int boardSize)
    {
        BoardSize = boardSize;
    }

    public int BoardSize { get; set; }
}