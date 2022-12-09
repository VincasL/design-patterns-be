using BattleshipsApi.Composite;
using BattleshipsApi.Enums;
using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Entities.Ships
{
    public class Submarine : Ship, IVisitable
    {
        public Submarine()
        {
            Length = 3;
            Type = ShipType.Submarine;
        }
        
        private Submarine(ShipType type, bool isHorizontal, int armourStrength, int fuel, List<IShipComponent> components): base(type, isHorizontal, armourStrength, fuel, components)
        {
        }

        public int Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public override Ship Clone()
        {
            return new Submarine(Type, IsHorizontal, ArmourStrength, Fuel, new List<IShipComponent>(Children));
        }
    }
}
