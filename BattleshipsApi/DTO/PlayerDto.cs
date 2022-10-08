namespace BattleshipsApi.DTO;

public class PlayerDto
{
    public string ConnectionId { get; set; }
    public string Name { get; set; }
    public BoardDto Board { get; set; }
    public bool PlacedShips { get; set; }
}