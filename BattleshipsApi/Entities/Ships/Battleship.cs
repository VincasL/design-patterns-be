using BattleshipsApi.Enums;

namespace BattleshipsApi.Entities.Ships
{
    public class Battleship : Ship
    {
        public Battleship()
        {
            Length = 4;
            Type = ShipType.Battleship;
        }
    }
}
