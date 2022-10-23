using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public abstract class Ship
{
    public ShipType Type { get; set; }
    public bool IsHorizontal { get; set; }
    public int ShieldStrength { get; set; }
    public int ArmourStrength { get; set; }
    public int Fuel { get; set; }
    public int Length { get; set; }
}