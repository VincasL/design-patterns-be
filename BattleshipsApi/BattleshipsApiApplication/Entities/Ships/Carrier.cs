using BattleshipsApi.Composite;
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

        private Carrier(ShipType type, bool isHorizontal, int armourStrength, int fuel, List<IShipComponent> components): base(type, isHorizontal, armourStrength, fuel, components)
        {
        }


        public override Ship Clone()
        {
            return new Carrier(Type, IsHorizontal, ArmourStrength, Fuel, new List<IShipComponent>(Children));
        }
    }
}
