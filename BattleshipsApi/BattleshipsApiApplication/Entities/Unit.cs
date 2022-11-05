using BattleshipsApi.Strategies;

namespace BattleshipsApi.Entities
{
    public abstract class Unit
    {

        public MoveStrategy MoveStrategy=new DontMove();

        public abstract Unit Clone();

    }
}
