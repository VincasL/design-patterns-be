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
        
        private Destroyer(ShipType type, bool isHorizontal, int armourStrength, int fuel): base(type, isHorizontal, armourStrength, fuel)
        {
        }


        public override Ship Clone()
        {
            return new Destroyer(Type, IsHorizontal, ArmourStrength, Fuel);
        }
    }
}
