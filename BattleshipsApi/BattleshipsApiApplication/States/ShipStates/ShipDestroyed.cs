using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.States.ShipStates
{
    public class ShipDestroyed : IShipState
    {
        public override void HandleState(Ship ship)
        {
            fuel = 0;
            shield = 0;
            rockets = 0;
            drone = null;
            crew = null;
            return;
        }

    }
}
