using BattleshipsApi.Entities;

namespace BattleshipsApi.DTO;

public class PlayerDto
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }
    public BoardDto Board { get; set; }
    public List<Ship> PlacedShips { get; set; }
    public List<Mine> PlacedMines { get; set; }

    public bool AreAllShipsPlaced { get; set; }
}