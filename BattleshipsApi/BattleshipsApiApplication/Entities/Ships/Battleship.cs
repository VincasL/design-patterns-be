using BattleshipsApi.Composite;
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

        private Battleship(ShipType type, bool isHorizontal, int armourStrength, int fuel, List<IShipComponent> components): base(type, isHorizontal, armourStrength, fuel, components)
        {
        }


        public override Ship Clone()
        {
            return new Battleship(Type, IsHorizontal, ArmourStrength, Fuel, new List<IShipComponent>(Children));
        }
    }
}
