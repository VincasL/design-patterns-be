using BattleshipsApi.Entities;

namespace BattleshipsApi.Strategies
{
    public class DontMove : MoveStrategy
    {
        public override void MoveDifferently(Board board, Unit unit)
        {
            throw new NotImplementedException();
        }
    }
}
