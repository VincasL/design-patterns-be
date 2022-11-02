using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public class Player
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }
    public NationType nationType {get;set;}
    public Board Board { get; set; }
    public List<Ship> PlacedShips { get; set; }
    public List<Mine> PlacedMines { get; set; } 
    public bool AreAllUnitsPlaced { get; set; }
    public bool Winner { get; set; }

    public Player(string connectionId, string name)
    {
        ConnectionId = connectionId;
        Name = name;
        PlacedMines = new List<Mine>();
        PlacedShips = new List<Ship>();
    }

    public Player(string connectionId, string name, Board board, bool areAllUnitsPlaced, bool winner, List<Ship> placedShips, List<Mine> placedMines)
    {
        ConnectionId = connectionId;
        Name = name;
        Board = board;
        AreAllUnitsPlaced = areAllUnitsPlaced;
        Winner = winner;
        PlacedShips = placedShips;
        PlacedMines = placedMines;
    }
}