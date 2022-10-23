using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Ships
{
    public class Destroyer:Ship
    {
        public Destroyer()
        {
            Length = 2;
            Type = ShipType.Destroyer;
        }
    }
}
