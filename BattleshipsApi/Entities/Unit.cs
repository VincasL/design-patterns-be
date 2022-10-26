using BattleshipsApi.Strategies;

namespace BattleshipsApi.Entities
{
    public class Unit: ICloneable
    {
        public int Length { get; set; }
        public MoveStrategy MoveStrategy=new DontMove();

        public object Clone()
        {
            return (Unit)MemberwiseClone();
        }
    }
}
