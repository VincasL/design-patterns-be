using BattleshipsApi.Strategies;

namespace BattleshipsApi.Entities
{
    public class Unit
    {
        public int Length { get; set; }
        public MoveStrategy MoveStrategy=new DontMove();
    }
}
