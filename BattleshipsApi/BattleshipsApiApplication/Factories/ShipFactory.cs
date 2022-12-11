using BattleshipsApi.Entities;
using BattleshipsApi.Entities.Ships;
using BattleshipsApi.Enums;

namespace BattleshipsApi.Factories;

public class ShipFactory
{
    public Ship GetShip(ShipType type)
    {
        switch (type)
        {
            case ShipType.Battleship:
                return new Battleship();

            case ShipType.Carrier:
                return new Carrier();

            case ShipType.Cruiser:
                return new Cruiser();

            case ShipType.Destroyer:
                return new Destroyer();

            case ShipType.Submarine:
                return new Submarine();

            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }
}