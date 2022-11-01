using BattleshipsApi.Enums;
using BattleshipsApi.Strategies;

namespace BattleshipsApi.Entities;

public abstract class Ship : Unit
{
    public ShipType Type { get; set; }
    public bool IsHorizontal { get; set; }

    public int ArmourStrength { get; set; }
    public int Fuel { get; set; }
}
