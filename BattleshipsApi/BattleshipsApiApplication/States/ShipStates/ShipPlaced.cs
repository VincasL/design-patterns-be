using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.States.ShipStates
{
    public class ShipPlaced : IShipState
    {
        public override void HandleState(Ship ship)
        {
            ship.ShipState = new ShipDammaged();
            dashSpeed = 2;
            fuel = 6;
            shield = 1;
            rockets = 3;
            drone = new Drone();
            crew = new CrewPersonel();
        }

    }
}
