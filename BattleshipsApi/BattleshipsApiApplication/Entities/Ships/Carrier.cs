using BattleshipsApi.Composite;
using BattleshipsApi.Enums;
using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Entities.Ships
{
    public class Carrier : Ship, IVisitable
    {
        public Carrier()
        {
            Length = 5;
            Type = ShipType.Carrier;
        }

        private Carrier(ShipType type, bool isHorizontal, int armourStrength, int fuel, List<IShipComponent> components): base(type, isHorizontal, armourStrength, fuel, components)
        {
        }
        public int Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }


        public override Ship Clone()
        {
            return new Carrier(Type, IsHorizontal, ArmourStrength, Fuel, new List<IShipComponent>(Children));
        }
    }
}
