namespace BattleshipsApi.Entities;

public class GamePlayer
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }

    public bool HasPlacedMines { get; set; } = false;

    public GamePlayer(string connectionId, string name)
    {
        ConnectionId = connectionId;
        Name = name;
    }
}