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
        
        private Submarine(ShipType type, bool isHorizontal, int armourStrength, int fuel): base(type, isHorizontal, armourStrength, fuel)
        {
        }


        public override Ship Clone()
        {
            return new Submarine(Type, IsHorizontal, ArmourStrength, Fuel);
        }
    }
}
