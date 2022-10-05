namespace BattleshipsApi.Entities;

public class GamePlayer
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }

    public List<GameShip> Ships { get; set; } = new();

    public GamePlayer(string connectionId, string name)
    {
        ConnectionId = connectionId;
        Name = name;
    }
}