using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Handlers;

namespace BattleshipsApi.Helpers;

public static class Helpers
{
    public static int GetShipLength(this ShipType shipType)
    {
        switch (shipType)
        {
            case ShipType.Carrier:
                return 5;
            case ShipType.Battleship:
                return 4;
            case ShipType.Cruiser:
            case ShipType.Submarine:
                return 3;
            case ShipType.Destroyer:
                return 2;
            default:
                throw new ArgumentOutOfRangeException(nameof(shipType), shipType, null);
        }
    }
    

    
}