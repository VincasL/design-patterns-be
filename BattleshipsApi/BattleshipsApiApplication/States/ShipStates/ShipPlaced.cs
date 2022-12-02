using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.States.ShipStates
{
    public class ShipPlaced : IShipState
    {
        public void HandleState(Ship ship)
        {
            ship.ShipState = new ShipDammaged();
        }
    }
}
