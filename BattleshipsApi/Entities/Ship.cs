using BattleshipsApi.Entities.Ships;
using BattleshipsApi.Enums;
using BattleshipsApi.Strategies;

namespace BattleshipsApi.Entities;

public abstract class Ship : Unit
{
    public int Length { get; set; }
    public ShipType Type { get; set; }
    public bool IsHorizontal { get; set; }

    public int ArmourStrength { get; set; }
    public int Fuel { get; set; }

    protected Ship(ShipType type, bool isHorizontal, int armourStrength, int fuel)
    {
        Type = type;
        IsHorizontal = isHorizontal;
        ArmourStrength = armourStrength;
        Fuel = fuel;
    }

    protected Ship()
    {
    }
}
