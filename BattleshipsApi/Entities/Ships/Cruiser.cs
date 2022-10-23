using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Ships
{
    public class Cruiser : Ship
    {
        public Cruiser()
        {
            Length = 3;
            Type = ShipType.Cruiser;
        }
    }
}
