using BattleshipsApi.Composite;
using BattleshipsApi.Contracts;
using BattleshipsApi.Enums;
using BattleshipsApi.Proxy;
using BattleshipsApi.States.ShipStates;
using BattleshipsApi.Template;
using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Entities;

public abstract class Ship : Unit, IShipComponent, IGetShipData, IPlaceItem
{
    public int Length { get; set; }
    public ShipType Type { get; set; }
    public bool IsHorizontal { get; set; }
    public int ArmourStrength { get; set; }
    public int Fuel { get; set; }
    public int Speed { get; set; }
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
    public Ship(){ }

    public int GetArmourStrength()
    {
        return Children.Aggregate(0, (acc, x) => acc + x.GetArmourStrength());
    }

    public int GetShipSize(ShipType type, NationType nationType)
    {
        return Length;
    }

    public override Unit Clone()
    {
        throw new NotImplementedException();
    }

   // public abstract int Accept(IVisitor visitor);

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
