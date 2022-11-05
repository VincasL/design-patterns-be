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
        
        private Cruiser(ShipType type, bool isHorizontal, int armourStrength, int fuel): base(type, isHorizontal, armourStrength, fuel)
        {
        }


        public override Ship Clone()
        {
            return new Cruiser(Type, IsHorizontal, ArmourStrength, Fuel);
        }
    }
    
}
