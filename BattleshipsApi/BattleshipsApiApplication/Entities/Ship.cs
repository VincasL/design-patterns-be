using BattleshipsApi.Composite;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities;

public abstract class Ship : Unit, IShipComponent
{
    public int Length { get; set; }
    public ShipType Type { get; set; }
    public bool IsHorizontal { get; set; }

    public int ArmourStrength { get; set; }
    public int Fuel { get; set; }

    public List<IShipComponent> Children { get; set; } = new(); 

    protected Ship(ShipType type, bool isHorizontal, int armourStrength, int fuel, List<IShipComponent> children)
    {
        Type = type;
        IsHorizontal = isHorizontal;
        ArmourStrength = armourStrength;
        Fuel = fuel;
        Children = children;
    }

    protected Ship()
    {
    }

    public int GetArmourStrength()
    {
        return Children.Aggregate(0, (acc, x) => acc + x.GetArmourStrength());
    }

    public int GetFuel()
    {
        return Children.Aggregate(0, (acc, x) => acc + x.GetFuel());
    }
}
