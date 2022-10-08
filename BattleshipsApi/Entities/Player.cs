namespace BattleshipsApi.Entities;

public class Player
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }
    public Board Board { get; set; }
    public bool PlacedShips { get; set; }
    public int DestroyedShipCount { get; set; }

    public Player(string connectionId, string name)
    {
        ConnectionId = connectionId;
        Name = name;
    }
}