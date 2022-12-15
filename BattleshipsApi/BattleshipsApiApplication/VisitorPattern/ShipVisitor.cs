using BattleshipsApi.Entities;

namespace BattleshipsApi.VisitorPattern
{
    public class ShipVisitor : IVisitor
    {
        private readonly double timeNow;
        public ShipVisitor(DateTime timeGameStarted) {
            timeNow = (DateTime.UtcNow - timeGameStarted).TotalSeconds;
        }
        public int Visit(IVisitable visitable)
        {
            if (visitable is Ship ship)
            {
                if (timeNow > 0 && timeNow < 30)
                {
                    return ship.Speed = 10;
                }
                if (timeNow >= 30 && timeNow < 120)
                {
                    return ship.Speed = 2;
                }
                if (timeNow >= 120 && timeNow < 300)
                {
                    return ship.Speed = 1;
                }
            }
            return 0;
        }
    }
}
