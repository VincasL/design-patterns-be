using BattleshipsApi.Enums;

namespace BattleshipsApi.DTO;

public class CellDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public CellType Type { get; set; }
}