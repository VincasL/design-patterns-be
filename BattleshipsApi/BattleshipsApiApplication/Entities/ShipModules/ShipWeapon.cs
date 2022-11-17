using BattleshipsApi.Composite;

namespace BattleshipsApi.Entities.ShipModules;

public interface IShipWeapon : IShipComponent
{
    public int Damage();
}