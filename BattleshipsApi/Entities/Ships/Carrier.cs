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

        private Carrier(ShipType type, bool isHorizontal, int armourStrength, int fuel): base(type, isHorizontal, armourStrength, fuel)
        {
        }


        public override Ship Clone()
        {
            return new Carrier(Type, IsHorizontal, ArmourStrength, Fuel);
        }
    }
}
