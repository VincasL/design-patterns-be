using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.States.ShipStates
{
    public class ShipDammaged : IShipState
    {
        public void HandleState(Ship ship)
        {
            if (ship.ArmourStrength > 0)
            {
                ship.ArmourStrength -= 1;
            }
            else
            {
                ship.ShipState = new ShipDestroyed();
            }
        }
    }
}
