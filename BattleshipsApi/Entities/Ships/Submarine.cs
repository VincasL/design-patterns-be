using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Ships
{
    public class Submarine : Ship
    {
        public Submarine()
        {
            Length = 3;
            Type = ShipType.Submarine;

        }
    }
}
