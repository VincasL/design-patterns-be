using BattleshipsApi.Entities;
using BattleshipsApi.Enums;
using BattleshipsApi.Factories;

namespace BattleshipsApi.Proxy
{
    public class ShipProxy : IGetShipData
    {
        private Ship ship;
        public int GetShipSize(ShipType type, NationType nationType)
        {
            if (ship == null)
            {
                ship = new AbstractFactory().CreateShip(type, nationType);
            }
            return ship.GetShipSize(type, nationType);
        }
    }
}
