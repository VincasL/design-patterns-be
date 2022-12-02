using BattleshipsApi.Contracts;
using BattleshipsApi.Entities;

namespace BattleshipsApi.States.ShipStates
{
    public class ShipNotPlaced : IShipState
    {
        public void HandleState(Ship ship)
        {
            ship.ShipState = new ShipPlaced();
        }
    }
}
