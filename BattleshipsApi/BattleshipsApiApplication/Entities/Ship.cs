using BattleshipsApi.Composite;
using BattleshipsApi.Contracts;
using BattleshipsApi.Enums;
using BattleshipsApi.States.ShipStates;
using BattleshipsApi.Template;
using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Entities;

public abstract class Ship : Unit, IShipComponent, IPlaceItem, IVisitable
{
    public int Length { get; set; }
    public ShipType Type { get; set; }
    public bool IsHorizontal { get; set; }
    public int ArmourStrength { get; set; }
    public int TotalArmorStrength => GetArmourStrength();
    public int Fuel { get; set; }
    public int Speed { get; set; }
    IShipState State;
    // Gets or sets the State
    public IShipState ShipState
    {
        get { return State; }
        set
        {
            State = value;
        }
    }
    public void Request()
    {
        State.HandleState(this);
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
    protected Ship(){ }

    public int GetArmourStrength()
    {
        return Children.Aggregate(0, (acc, x) => acc + x.GetArmourStrength());
    }

    public override Unit Clone()
    {
        throw new NotImplementedException();
    }

    public void PlaceObject(Unit unit, Board board, CellCoordinates coordinates)
    {
        var ship = (Ship)unit;
        if (ship.IsHorizontal)
        {
            for (var y = coordinates.Y; y < coordinates.Y + ship.Length; y++)
            {
                var cell = board.Cells[coordinates.X, y];

                if (cell.Ship != null)
                {
                    throw new Exception("Ships overlap");
                }
                cell.Ship = ship;
            }
        }
        else
        {
            for (var x = coordinates.X; x < coordinates.X + ship.Length; x++)
            {
                var cell = board.Cells[x, coordinates.Y];

                if (cell.Ship != null)
                {
                    throw new Exception("Ships overlap");
                }
                cell.Ship = ship;
            }
        }
    }

    public void ObjectExists(Unit unit)
    {
        if (unit == null)
        {
            throw new Exception("Ship doesn't exist");
        }
    }

    public abstract int Accept(IVisitor visitor);
}
