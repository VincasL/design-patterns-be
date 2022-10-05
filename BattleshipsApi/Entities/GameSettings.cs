namespace BattleshipsApi.Entities;

public class GameSettings
{
    public GameSettings(int boardSize)
    {
        BoardSize = boardSize;
    }

    public int BoardSize { get; set; }
}