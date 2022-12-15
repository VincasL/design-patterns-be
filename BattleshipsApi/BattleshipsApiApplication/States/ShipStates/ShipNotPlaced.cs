using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.States.ShipStates
{
    public class ShipNotPlaced : IShipState
    {
        public override void HandleState(Ship ship)
        {
            ship.ShipState = new ShipPlaced();
            dashSpeed = 0;
            fuel = 0;
            shield = 0;
            rockets = 0;
        }
    }
}
