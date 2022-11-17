using BattleshipsApi.Composite;
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
        
        private Submarine(ShipType type, bool isHorizontal, int armourStrength, int fuel, List<IShipComponent> components): base(type, isHorizontal, armourStrength, fuel, components)
        {
        }


        public override Ship Clone()
        {
            return new Submarine(Type, IsHorizontal, ArmourStrength, Fuel, new List<IShipComponent>(Children));
        }
    }
}
