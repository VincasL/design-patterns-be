using BattleshipsApi.Composite;
using BattleshipsApi.Contracts;
using BattleshipsApi.Enums;
using BattleshipsApi.States.ShipStates;

namespace BattleshipsApi.Entities;

public abstract class Ship : Unit, IShipComponent
{
    public int Length { get; set; }
    public ShipType Type { get; set; }
    public bool IsHorizontal { get; set; }
    public int ArmourStrength { get; set; }
    public int Fuel { get; set; }
    IShipState state;
    // Gets or sets the state
    public IShipState ShipState
    {
        get { return state; }
        set
        {
            state = value;
        }
    }
    public void Request()
    {
        state.HandleState(this);
    }

    public List<IShipComponent> Children { get; set; } = new(); 

    protected Ship(ShipType type, bool isHorizontal, int armourStrength, int fuel, List<IShipComponent> children)
    {
        Type = type;
        IsHorizontal = isHorizontal;
        ArmourStrength = armourStrength;
        Fuel = fuel;
        Children = children;
        this.ShipState = new ShipNotPlaced();
    }
    public Ship()
    {

    }

    public int GetArmourStrength()
    {
        return Children.Aggregate(0, (acc, x) => acc + x.GetArmourStrength());
    }
}
