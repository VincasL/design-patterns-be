using BattleshipsApi.Entities;
using BattleshipsApi.States.ShipStates;

namespace BattleshipsApi.Contracts
{
    public abstract class IShipState
    {
        public abstract void HandleState(Ship ship);
        protected int dashSpeed;
        protected int fuel;
        protected int shield;
        protected int rockets;
        protected Drone? drone;
        protected CrewPersonel? crew;
    }
}
