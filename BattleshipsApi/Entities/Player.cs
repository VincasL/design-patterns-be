namespace BattleshipsApi.Entities;

public class Player
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }
    public Board Board { get; set; }
    public List<Ship> PlacedShips { get; set; } = new List<Ship>();
    public bool AreAllShipsPlaced { get; set; }
    public int DestroyedShipCount { get; set; }
    public bool Winner { get; set; }

    public Player(string connectionId, string name)
    {
        ConnectionId = connectionId;
        Name = name;
    }
}