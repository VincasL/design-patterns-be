using BattleshipsApi.Entities;

namespace BattleshipsApi.Contracts
{
    public interface IShipState
    {
        public abstract void HandleState(Ship ship);
    }
}
