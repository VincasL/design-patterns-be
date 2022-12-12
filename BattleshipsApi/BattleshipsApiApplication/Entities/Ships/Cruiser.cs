using BattleshipsApi.Composite;
using BattleshipsApi.Enums;
using BattleshipsApi.VisitorPattern;

namespace BattleshipsApi.Entities.Ships
{
    public class Cruiser : Ship, IVisitable
    {
        public Cruiser()
        {
            Length = 3;
            Type = ShipType.Cruiser;
        }
        
        private Cruiser(ShipType type, bool isHorizontal, int armourStrength, int fuel, List<IShipComponent> components): base(type, isHorizontal, armourStrength, fuel, components)
        {
        }
        public override int Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }
        public override Ship Clone()
        {
            return new Cruiser(Type, IsHorizontal, ArmourStrength, Fuel, new List<IShipComponent>(Children));
        }
    }
    
}
