using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public class Ship
{
    public ShipType Type { get; set; }
    public Cell Cell { get; set; }
    public bool IsHorizontal { get; set; }
}