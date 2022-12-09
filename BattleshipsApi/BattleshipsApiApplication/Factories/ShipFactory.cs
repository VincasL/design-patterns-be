using BattleshipsApi.Entities;
using BattleshipsApi.Entities.Ships;
using BattleshipsApi.Enums;
using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Factories;

public class ShipFactory
{
    public Ship GetShip(ShipType type)
    {
        var shipVisitor = new ShipVisitor();
        switch (type)
        {
            case ShipType.Battleship:
                var battleship = new Battleship();
                battleship.Accept(shipVisitor);
                return battleship;
            case ShipType.Carrier:
                var carrier = new Carrier();
                carrier.Accept(shipVisitor);
                return carrier;
            case ShipType.Cruiser:
                var cruiser = new Cruiser();
                cruiser.Accept(shipVisitor);
                return cruiser;
            case ShipType.Destroyer:
                var destroyer = new Destroyer();
                destroyer.Accept(shipVisitor);
                return destroyer;
            case ShipType.Submarine:
                var submarine = new Submarine();
                submarine.Accept(shipVisitor);
                return submarine;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}