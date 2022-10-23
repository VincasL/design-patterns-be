using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Ships
{
    public class Carrier : Ship
    {
        public Carrier()
        {
            Length = 5;
            Type = ShipType.Carrier;
        }
    }
}
