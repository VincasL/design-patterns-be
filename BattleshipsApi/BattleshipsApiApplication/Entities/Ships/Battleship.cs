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

        private Battleship(ShipType type, bool isHorizontal, int armourStrength, int fuel): base(type, isHorizontal, armourStrength, fuel)
        {
        }


        public override Ship Clone()
        {
            return new Battleship(Type, IsHorizontal, ArmourStrength, Fuel);
        }
    }
}
